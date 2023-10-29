using System.Security.Claims;
using GamingGroove.Data;
using GamingGroove.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Modes;

namespace GamingGroove.Views.ComunidadePage
{
    public class ComunidadePageViewModel : PageModel
    {
        private readonly GamingGrooveDbContext _cc;

        public ComunidadePageViewModel(GamingGrooveDbContext cc)
        {
            _cc = cc;
        }

        public UsuarioModel getUsuario { get; set; }
        public ComunidadeModel getComunidade { get; set; }
        public List<ComunidadeModel> getComunidadesUsuario { get; set; }
        public List<ComunidadeModel> getTodasComunidades { get; set; }
        public IEnumerable<UsuarioComunidadeModel> getComunidades { get; set; }
        public List<string> infoComunidades { get; set; }

        public int IdUsuarioLogado { get; set; }

        public void OnGet(string community)
        {
            
            getComunidade = _cc.Comunidades.FirstOrDefault(u => u.nomeComunidade == community);

            getTodasComunidades = _cc.Comunidades.ToList();
        }

        public int GetNumberOfMembersInCommunity(int _comunidadeId)
        {
            return _cc.UsuariosComunidades
                .Count(uc => uc.comunidadeId == _comunidadeId);
        }

        public void GetUsuarioLogado (int usuarioLogado)
        {            
            IdUsuarioLogado = usuarioLogado;

            getUsuario = _cc.Usuarios.FirstOrDefault(u => u.usuarioId == IdUsuarioLogado);
                        getComunidades = _cc.UsuariosComunidades
                .Where(uc => uc.usuarioId != IdUsuarioLogado)
                .Include(uc => uc.comunidade)
                .ToList();

            infoComunidades = getComunidades.Select(ue => ue.comunidade.nomeComunidade).ToList();
        }
    }
}