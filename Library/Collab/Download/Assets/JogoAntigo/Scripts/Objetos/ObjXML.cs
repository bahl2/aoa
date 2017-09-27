using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

namespace Assets.Scripts.Objetos
{
    public static class XML
    {
        public static TiposConstrucoes[] ParseTiposConstrucoes(string pConteudo)
        {
            XmlReader lLeitor = XmlReader.Create(new StringReader(pConteudo));
            List<TiposConstrucoes> lTiposConstrucoes = new List<TiposConstrucoes>();
            TiposConstrucoes lTipoConstrucaoAtual = null;
            while (lLeitor.Read())
            {
                if (lLeitor.IsStartElement("TipoConstrucao"))
                {
                    if (lTipoConstrucaoAtual != null)
                    {
                        lTiposConstrucoes.Add(lTipoConstrucaoAtual);
                    }
                    lTipoConstrucaoAtual = new TiposConstrucoes();
                }
                if (lTipoConstrucaoAtual != null)
                {
                    if (lLeitor.IsStartElement("IdTiposConstrucoes"))
                    {
                        lTipoConstrucaoAtual._IdTiposConstrucoes = lLeitor.ReadElementContentAsInt();
                    }
                    if (lLeitor.IsStartElement("Descricao"))
                    {
                        lTipoConstrucaoAtual._Descricao = lLeitor.ReadElementContentAsString();
                        TiposPovos lTipoPovo = GerenciadorTiposPovos.Instancia.GetTipoPovoUsuarioLogado();
                        string lCaminhoPrincipal = string.Format("Construcoes/{0}/", lTipoPovo._Descricao);
                        lTipoConstrucaoAtual._Prefab = Resources.Load<Transform>(lCaminhoPrincipal + lTipoConstrucaoAtual._Descricao);
                        lTipoConstrucaoAtual._Foto = Resources.Load<Transform>(lCaminhoPrincipal + lTipoConstrucaoAtual._Descricao).
                            GetComponent<SpriteRenderer>().sprite;
                    }
                    if (lLeitor.IsStartElement("Valor"))
                    {
                        lTipoConstrucaoAtual._Cristais = int.Parse(lLeitor.GetAttribute("Cristais"));
                        lTipoConstrucaoAtual._Madeira = int.Parse(lLeitor.GetAttribute("Madeira"));
                        lTipoConstrucaoAtual._Aldeoes = int.Parse(lLeitor.GetAttribute("Aldeoes"));
                    }
                    if (lLeitor.IsStartElement("Segundos"))
                    {
                        lTipoConstrucaoAtual._Segundos = lLeitor.ReadElementContentAsFloat();
                    }
                }
            }
            if (lTipoConstrucaoAtual != null)
            {
                lTiposConstrucoes.Add(lTipoConstrucaoAtual);
            }
            return lTiposConstrucoes.ToArray();
        }

        public static TiposRecursos[] ParseTiposRecursos(string pConteudo)
        {
            XmlReader lLeitor = XmlReader.Create(new StringReader(pConteudo));
            List<TiposRecursos> lTiposRecursos = new List<TiposRecursos>();
            TiposRecursos lTipoRecursoAtual = null;
            while (lLeitor.Read())
            {
                if (lLeitor.IsStartElement("TipoRecurso"))
                {
                    if (lTipoRecursoAtual != null)
                    {
                        lTiposRecursos.Add(lTipoRecursoAtual);

                    }
                    lTipoRecursoAtual = new TiposRecursos();
                }
                if (lTipoRecursoAtual != null)
                {
                    if (lLeitor.IsStartElement("IdTiposRecursos"))
                    {
                        lTipoRecursoAtual._IdTiposRecursos = lLeitor.ReadElementContentAsInt();
                    }
                    if (lLeitor.IsStartElement("Descricao"))
                    {
                        lTipoRecursoAtual._Descricao = lLeitor.ReadElementContentAsString();
                        string lCaminhoPrincipal = string.Format("Recursos/{0}", lTipoRecursoAtual._Descricao);
                        lTipoRecursoAtual._Vazio = Resources.Load<Transform>(string.Format("{0}/Vazio", lCaminhoPrincipal));
                        lTipoRecursoAtual._Cheio = Resources.Load<Transform>(string.Format("{0}/Cheio", lCaminhoPrincipal));
                    }
                }
            }
            if (lTipoRecursoAtual != null)
            {
                lTiposRecursos.Add(lTipoRecursoAtual);
            }
            return lTiposRecursos.ToArray();
        }

        public static TiposUnidadesMoveis[] ParseTiposUnidadesMoveis(string pConteudo)
        {
            XmlReader lLeitor = XmlReader.Create(new StringReader(pConteudo));
            List<TiposUnidadesMoveis> lTiposUnidadesMoveis = new List<TiposUnidadesMoveis>();
            TiposUnidadesMoveis lTipoUnidadesMoveisAtual = null;
            while (lLeitor.Read())
            {
                if (lLeitor.IsStartElement("TipoUnidadeMovel"))
                {
                    if (lTipoUnidadesMoveisAtual != null)
                    {
                        lTiposUnidadesMoveis.Add(lTipoUnidadesMoveisAtual);
                    }
                    lTipoUnidadesMoveisAtual = new TiposUnidadesMoveis();
                }
                if (lTipoUnidadesMoveisAtual != null)
                {
                    if (lLeitor.IsStartElement("IdTiposUnidadesMoveis"))
                    {
                        lTipoUnidadesMoveisAtual._IdTiposUnidadesMoveis = lLeitor.ReadElementContentAsInt();
                    }
                    if (lLeitor.IsStartElement("Descricao"))
                    {
                        lTipoUnidadesMoveisAtual._Descricao = lLeitor.ReadElementContentAsString();
                        Transform lPrefabFoto = Resources.Load<Transform>(string.Format("UnidadesMoveis/{0}", lTipoUnidadesMoveisAtual._Descricao));
                        lTipoUnidadesMoveisAtual._Foto = lPrefabFoto.GetComponent<SpriteRenderer>().sprite;
                    }
                    if (lLeitor.IsStartElement("IdTiposConstrucoes"))
                    {
                        lTipoUnidadesMoveisAtual._IdTiposConstrucoes = lLeitor.ReadElementContentAsInt();
                    }
                    if (lLeitor.IsStartElement("Valor"))
                    {
                        lTipoUnidadesMoveisAtual._Cristais = int.Parse(lLeitor.GetAttribute("Cristal"));
                        lTipoUnidadesMoveisAtual._Madeira = int.Parse(lLeitor.GetAttribute("Madeira"));
                    }
                }
            }
            if (lTipoUnidadesMoveisAtual != null)
            {
                lTiposUnidadesMoveis.Add(lTipoUnidadesMoveisAtual);
            }
            return lTiposUnidadesMoveis.ToArray();
        }

        public static TiposPovos[] ParseTiposPovos(string pConteudo)
        {
            XmlReader lLeitor = XmlReader.Create(new StringReader(pConteudo));
            List<TiposPovos> lTiposPovos = new List<TiposPovos>();
            TiposPovos lTipoPovoAtual = null;
            while (lLeitor.Read())
            {
                if (lLeitor.IsStartElement("TipoPovo"))
                {
                    if (lTipoPovoAtual != null)
                    {
                        lTiposPovos.Add(lTipoPovoAtual);

                    }
                    lTipoPovoAtual = new TiposPovos();
                }
                if (lTipoPovoAtual != null)
                {
                    if (lLeitor.IsStartElement("IdTiposPovos"))
                    {
                        lTipoPovoAtual._IdTiposPovos = lLeitor.ReadElementContentAsInt();
                    }
                    if (lLeitor.IsStartElement("Descricao"))
                    {
                        lTipoPovoAtual._Descricao = lLeitor.ReadElementContentAsString();
                    }
                }
            }
            if (lTipoPovoAtual != null)
            {
                lTiposPovos.Add(lTipoPovoAtual);
            }
            return lTiposPovos.ToArray();
        }

        public static TiposHerois[] ParseTiposHerois(string pConteudo)
        {
            XmlReader lLeitor = XmlReader.Create(new StringReader(pConteudo));
            List<TiposHerois> lTiposHerois = new List<TiposHerois>();
            TiposHerois lTipoHeroiAtual = null;
            while (lLeitor.Read())
            {
                if (lLeitor.IsStartElement("TipoHeroi"))
                {
                    if (lTipoHeroiAtual != null)
                    {
                        lTiposHerois.Add(lTipoHeroiAtual);

                    }
                    lTipoHeroiAtual = new TiposHerois();
                }
                if (lTipoHeroiAtual != null)
                {
                    if (lLeitor.IsStartElement("IdTiposPovos"))
                    {
                        lTipoHeroiAtual._IdTiposHerois = lLeitor.ReadElementContentAsInt();
                    }
                    if (lLeitor.IsStartElement("Descricao"))
                    {
                        lTipoHeroiAtual._Descricao = lLeitor.ReadElementContentAsString();
                        Transform lPrefabFoto = Resources.Load<Transform>(string.Format("Herois/{0}", lTipoHeroiAtual._Descricao));
                        lTipoHeroiAtual._Foto = lPrefabFoto.GetComponent<SpriteRenderer>().sprite;
                    }
                }
            }
            if (lTipoHeroiAtual != null)
            {
                lTiposHerois.Add(lTipoHeroiAtual);
            }
            return lTiposHerois.ToArray();
        }
    }
}
