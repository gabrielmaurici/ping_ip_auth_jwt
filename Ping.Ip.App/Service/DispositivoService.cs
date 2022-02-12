using Ping.Ip.Domain;
using Ping.Ip.Domain.Domain;
using Ping.Ip.Domain.Dto;
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

        public async Task<RetornaDispositivoDto> InserirDispositivo(DispositivoDto model)
        {
            try
            {
                var retorno = await _dispositivoRepository.VerificaDispositivoExistePorIp(model.Ip);

                if (retorno)
                {
                    Dispositivo dispositivo = new()
                    {
                        Guid = Guid.NewGuid(),
                        Nome = model.Nome,
                        Ip = model.Ip,
                        TipoDispositivo = model.TipoDispositivo
                    };

                    await _dispositivoRepository.InserirDispositivo(dispositivo);

                    return new RetornaDispositivoDto
                    {
                        Guid = dispositivo.Guid,
                        Nome = dispositivo.Nome,
                        TipoDispositivo = dispositivo.TipoDispositivo,
                        Ip = dispositivo.Ip,
                        Mensagem = "Dispositivo cadastrado com sucesso."
                    };
                }

                return new RetornaDispositivoDto { Mensagem = "Este Ip já está cadastrado." };
            } 
            catch
            {
                return new RetornaDispositivoDto { Mensagem = "Falha ao tentar cadastar dispositivo, tente novamente mais tarde." };
            }
        }
        
        public async Task<bool> AtualizarDispositivo(AtualizaDispositivoDto model)
        {
            try
            {
                var dispositivoBase = await _dispositivoRepository.ObterDispositivoPorId(model.Id);

                if (dispositivoBase == null)
                    return false;

                Dispositivo dispositivo = new Dispositivo
                {
                    Id = model.Id,
                    Guid = dispositivoBase.Guid,
                    Nome = model.Nome,
                    Ip = model.Ip,
                    TipoDispositivo = model.TipoDispositivo
                };

                await _dispositivoRepository.AtualizarDispositivo(dispositivo);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<RetornaPingIpDto>> ObterStatusDispositivos()
        {
            try
            {
                var dispositivos = await _dispositivoRepository.ListarDispositivos();

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

                return lista;
            } 
            catch
            {
                return new List<RetornaPingIpDto>();
            }
        }

        public async Task<bool> DeletarDispositivo(int id)
        {
            try
            {
                var dispositivo = await _dispositivoRepository.ObterDispositivoPorId(id);

                if (dispositivo == null)
                    return false;
                
                await _dispositivoRepository.DeletarDispositivo(dispositivo);

                return true;                    
            }
            catch
            {
                return false;
            }
        }
    }
}
