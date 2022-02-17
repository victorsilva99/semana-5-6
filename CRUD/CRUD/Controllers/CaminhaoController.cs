using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class CaminhaoController : Controller
    {

#region INTANCIANDO O BANCO DE DADOS
        private readonly ApplicationDbContext _database;

        public CaminhaoController(ApplicationDbContext context)
        {
            _database = context;
        }
        #endregion

#region VIEW INDEX
        public IActionResult Index()
        {
            var lista = _database.Caminhao.ToList(); // "lista" irá receber os dados da tabela pelo método "ToList"
            return View(lista); // retorna a view já com os dados atualizados
        }
        #endregion

#region VIEW DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var detalhes = await _database.Caminhao.FindAsync(id);
            return View(detalhes);
        }
        #endregion

#region VIEW CREATE
        public IActionResult Create()
        {
            return View();
        }

        /* Quando a Action Create é acionada o método async irá fazer a validação do ID para evitar erros de duplicidade de ID
         * e após isso salvará os dados*/
        [HttpPost]
        public async Task<IActionResult> Create(Caminhao novo)
        {
            if (_database.Caminhao. Any(registro => registro.IdCaminhao == novo.IdCaminhao))
            {
                ModelState.AddModelError("IdCaminhao", "Esse ID já está registrado.");
                return View(novo);
            }
            if (ModelState.IsValid)
            {
                _database.Add(novo);
                await _database.SaveChangesAsync();
                return RedirectToAction("Index");
            }
                return View(novo);
        }
        #endregion

#region VIEW EDIT
        public async Task<IActionResult> Edit(int id) // "id" irá receber o ID do caminhão que cliclamos em Editar
        {
            var obterDetalhes = await _database.Caminhao.FindAsync(id); // "obterDetalhes" vai armazenar as informações que serão exibidas na view, conforme o valor do parâmetro informado no método FindAsync() 
            return View(obterDetalhes);
        }

        /* Método que irá salvar a alteração, caso a validação do DataAnnotation esteja OK. O campo ID já é bloqueado dentro da View.*/
        [HttpPost]
        public async Task<IActionResult> Edit(Caminhao editar)
        {
            if (ModelState.IsValid)
            {
                _database.Update(editar);
                await _database.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(editar);
        }
        #endregion

#region VIEW DELETE
        public async Task<IActionResult> Delete(int? id) // "id" irá receber o ID do caminhão que cliclamos em Deletar
        {
            var obterDetalhes = await _database.Caminhao.FindAsync(id); // "obterDetalhes" vai armazenar as informações que serão exibidas na view, conforme o valor do parâmetro informado no método FindAsync() 
            return View(obterDetalhes);
        }
        /* Método que irá deletar o caminhão.*/
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var obterDetalhes = await _database.Caminhao.FindAsync(id);
            _database.Caminhao.Remove(obterDetalhes);
            await _database.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        #endregion
    }
}
