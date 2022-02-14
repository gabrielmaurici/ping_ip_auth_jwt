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
            try
            {
                var retorno = await _dispositivoRepository.VerificaDispositivoExistePorIp(model.Ip);

                if (!retorno)
                    return new RetornoGenericoModel<bool>() { Modelo = false, Mensagem = "Esse IP já está cadastrado, tente novamente mais tarde." };
                
                Dispositivo dispositivo = new()
                {
                    Guid = Guid.NewGuid(),
                    Nome = model.Nome,
                    Ip = model.Ip,
                    TipoDispositivo = model.TipoDispositivo
                };

                await _dispositivoRepository.InserirDispositivo(dispositivo);

                return new RetornoGenericoModel<bool>() { Modelo = true, Mensagem = "Dispositivo cadastrado com sucesso." };
            } 
            catch
            {
                return new RetornoGenericoModel<bool>() { Status = false };
            }
        }
        
        public async Task<RetornoGenericoModel<bool>> AtualizarDispositivo(AtualizaDispositivoDto model)
        {
            try
            {
                var dispositivoBase = await _dispositivoRepository.ObterDispositivoPorId(model.Id);

                if (dispositivoBase == null)
                    return new RetornoGenericoModel<bool>() { Modelo = false, Mensagem = "Dispositivo não econtrado." };

                Dispositivo dispositivo = new ()
                {
                    Id = model.Id,
                    Guid = dispositivoBase.Guid,
                    Nome = model.Nome,
                    Ip = model.Ip,
                    TipoDispositivo = model.TipoDispositivo
                };

                await _dispositivoRepository.AtualizarDispositivo(dispositivo);

                return new RetornoGenericoModel<bool>() { Modelo = true, Mensagem = "Dispositivo alterado com sucesso." };
            }
            catch
            {
                return new RetornoGenericoModel<bool>() { Status = false };
            }
        }

        public async Task<RetornoGenericoModel<List<RetornaPingIpDto>>> ObterStatusDispositivos()
        {
            try
            {
                var dispositivos = await _dispositivoRepository.ListarDispositivos();

                if(dispositivos.Count <= 0)
                    return new RetornoGenericoModel<List<RetornaPingIpDto>>() { Status = false, Mensagem = "Nenhum dispositivo cadastrado"};

                List<RetornaPingIpDto> lista = new ();

                foreach(var dispositivo in dispositivos)
                {
                    System.Net.NetworkInformation.Ping pinger = new System.Net.NetworkInformation.Ping();
                    PingReply resultado = await pinger.SendPingAsync(dispositivo.Ip);

                    bool status = true;
                    if (resultado.Status != 0)
                        status = false;

                    RetornaPingIpDto disp = new ()
                    {
                        Id = dispositivo.Id,
                        Nome = dispositivo.Nome,
                        Ip = dispositivo.Ip,
                        TipoDispositivo = dispositivo.TipoDispositivo,
                        Status = status
                    };


                    lista.Add(disp);
                }

                return new RetornoGenericoModel<List<RetornaPingIpDto>>() { Status = true, Modelo = lista };
            } 
            catch
            {
                return new RetornoGenericoModel<List<RetornaPingIpDto>>() { Status = false };
            }
        }

        public async Task<RetornoGenericoModel<bool>> DeletarDispositivo(int id)
        {
            try
            {
                var dispositivo = await _dispositivoRepository.ObterDispositivoPorId(id);

                if (dispositivo == null)
                    return new RetornoGenericoModel<bool>() { Modelo = false, Mensagem = "Dispositivo não encontrado." };
                
                await _dispositivoRepository.DeletarDispositivo(dispositivo);

                return new RetornoGenericoModel<bool>() { Modelo = true, Mensagem = "Dispositivo deletado com sucesso." };
            }
            catch
            {
                return new RetornoGenericoModel<bool>() { Status = false };
            }
        }
    }
}
