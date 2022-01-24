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
                var retorno = await _dispositivoRepository.ObterDispositivoPorIp(model.Ip);

                Dispositivo dispositivo = new Dispositivo
                {
                    Guid = Guid.NewGuid(),
                    Nome = model.Nome,
                    Ip = model.Ip
                };

                if (retorno)
                {
                    await _dispositivoRepository.InserirDispositivo(dispositivo);

                    return new RetornaDispositivoDto
                    {
                        Guid = dispositivo.Guid,
                        Nome = dispositivo.Nome,
                        Ip = dispositivo.Ip,
                        Mensagem = "Dispositivo cadastrado com sucesso."
                    };
                }

                return new RetornaDispositivoDto { Mensagem = "Este Ip já está cadastrado." };

            } catch
            {
                return new RetornaDispositivoDto { Mensagem = "Falha ao tentar cadastar dispositivo, tente novamente mais tarde." };
            }
        }

        public async Task<List<RetornaPingIpDto>> ObterStatusDispositivos()
        {
            try
            {
                var dispositivos = await _dispositivoRepository.ListarDispositivos();

                List<RetornaPingIpDto> lista = new List<RetornaPingIpDto>();

                foreach(var dispositivo in dispositivos)
                {
                    System.Net.NetworkInformation.Ping pinger = new System.Net.NetworkInformation.Ping();
                    PingReply resultado = await pinger.SendPingAsync(dispositivo.Ip);

                    bool status = true;
                    if (resultado.Status != 0)
                        status = false;

                    RetornaPingIpDto disp = new RetornaPingIpDto
                    {
                        Id = dispositivo.Id,
                        Nome = dispositivo.Nome,
                        Status = status
                    };


                    lista.Add(disp);
                }

                return lista;

            } catch
            {
                return new List<RetornaPingIpDto>();
            }
        }
    }
}
