using Ping.Ip.Domain;
using Ping.Ip.Domain.Domain;
using Ping.Ip.Domain.Dto;
using Ping.Ip.Domain.Model;
using Ping.Ip.Domain.Service;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Ping.Ip.App.Service
{
    public class DispositivoService : IDispositivoService
    {
        private readonly IDispositivoRepository _dispositivoRepository;

        public DispositivoService(IDispositivoRepository dispositivoRepository)
        {
            _dispositivoRepository = dispositivoRepository;
        }

        public async Task<RetornoGenericoModel<bool>> InserirDispositivo(DispositivoDto model)
        {
            var retorno = await _dispositivoRepository.VerificaDispositivoExistePorIp(model.Ip);
            if (retorno)
                return new RetornoGenericoModel<bool>("Esse IP já está cadastrado.");

            var dispositivo = new Dispositivo()
                .AdicionaDispositivo(model.Nome, model.TipoDispositivo, model.Ip);

            await _dispositivoRepository.InserirDispositivo(dispositivo);

            return new RetornoGenericoModel<bool>() { Status = true, Modelo = true, Mensagem = "Dispositivo cadastrado com sucesso." };
        }
        
        public async Task<RetornoGenericoModel<bool>> AtualizarDispositivo(AtualizaDispositivoDto model)
        {
            var dispositivoBase = await _dispositivoRepository.ObterDispositivoPorId(model.Id);
            if (dispositivoBase == null)
                return new RetornoGenericoModel<bool>("Dispositivo não econtrado.");

            var dispositivo = new Dispositivo()
                .AtualizaDispositivo(model.Nome, model.TipoDispositivo, model.Ip, dispositivoBase);

            await _dispositivoRepository.AtualizarDispositivo(dispositivo);

            return new RetornoGenericoModel<bool>() { Status = true, Modelo = true, Mensagem = "Dispositivo alterado com sucesso." };
        }

        public async Task<RetornoGenericoModel<List<RetornaPingIpDto>>> ObterStatusDispositivos()
        {
            var dispositivos = await _dispositivoRepository.ListarDispositivos();
            if (dispositivos.Count <= 0)
                return new RetornoGenericoModel<List<RetornaPingIpDto>>("Nenhum dispositivo cadastrado");

            List<RetornaPingIpDto> lista = new ();
            foreach (var dispositivo in dispositivos)
            {
                RetornaPingIpDto disp = new()
                {
                    Id = dispositivo.Id,
                    Nome = dispositivo.Nome,
                    Ip = dispositivo.Ip,
                    TipoDispositivo = dispositivo.TipoDispositivo,
                    Status = await RealizaPing(dispositivo.Ip)
                };

                lista.Add(disp);
            };

            return new RetornoGenericoModel<List<RetornaPingIpDto>>() { Status = true, Modelo = lista };
        }

        public async Task<RetornoGenericoModel<bool>> DeletarDispositivo(int id)
        {

            var dispositivo = await _dispositivoRepository.ObterDispositivoPorId(id);

            if (dispositivo == null)
                return new RetornoGenericoModel<bool>("Dispositivo não encontrado.");
                
            await _dispositivoRepository.DeletarDispositivo(dispositivo);

            return new RetornoGenericoModel<bool>() { Status = true, Modelo = true, Mensagem = "Dispositivo deletado com sucesso." };
        }

        private static async Task<bool> RealizaPing(string ip)
        {
            System.Net.NetworkInformation.Ping pinger = new();
            PingReply resultado = await pinger.SendPingAsync(ip);

            bool status = true;
            if (resultado.Status != 0)
                status = false;
            return status;
        }
    }
}
