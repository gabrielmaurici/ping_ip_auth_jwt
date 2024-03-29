﻿namespace Ping.Ip.Domain.Model
{
    public class RetornoGenericoModel<T>
    {
        public string Mensagem { get; set; }
        public bool Status { get; set; }
        public T Modelo { get; set; }

        public RetornoGenericoModel() { }

        public RetornoGenericoModel(T modelo, string mensagem = null)
        {
            Status = true;
            Modelo = modelo;
            Mensagem = mensagem;
        }

        public RetornoGenericoModel(string mensagemErro)
        {
            Status = false;
            Mensagem = mensagemErro;
        }
    }
}
