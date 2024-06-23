using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BibliotecaMvc.Models;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace BibliotecaMvc.Controllers
{
    public class ColecaoLivrosController : Controller
    {
        public string uriBase = "http://www.LivroLA.somee.com/ColecaoLivros/";

        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                string uriComplementar = "GetAll";
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + uriComplementar);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<ColecaoLivrosMvc> ListaPersonagens = await Task.Run(() =>
                    JsonConvert.DeserializeObject<List<ColecaoLivrosMvc>>(serialized));

                    return View(ListaPersonagens);
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {

                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        
        [HttpPost]
        public async Task<ActionResult> CreateAsync(ColecaoLivrosMvc p)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(JsonConvert.SerializeObject(p));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string.Format("Livro {0}, Id {1} salvo com sucesso!", p.NomedoLivro, serialized);
                    return RedirectToAction("Index");
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {

                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Create");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

    
        [HttpGet]
        public async Task<ActionResult> DetailsAsync(int? id)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());

                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ColecaoLivrosMvc p = await Task.Run(() =>
                    JsonConvert.DeserializeObject<ColecaoLivrosMvc>(serialized));
                    return View(p);
                }
                else
                    throw new System.Exception(serialized);

            }
            catch (System.Exception ex)
            {

                TempData["Mensagem"] = ex.Message;
                return RedirectToAction("Create");
            }
        }

        

        [HttpPost]
        public async Task<ActionResult> EditAsync(ColecaoLivrosMvc p)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var content = new StringContent(JsonConvert.SerializeObject(p));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await httpClient.PutAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] =
                    string.Format("Livro {0}, classe {1} atualizado com sucesso!", p.NomedoLivro, p.Genero);
                    return RedirectToAction("Index");

                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {

                TempData["Mensagem"] = ex.Message;
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public async Task<ActionResult> EditAsync(int? id)
        {

            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());

                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ColecaoLivrosMvc p = await Task.Run(() =>
                    JsonConvert.DeserializeObject<ColecaoLivrosMvc>(serialized));
                    return View(p);
                }
                else
                    throw new System.Exception(serialized);

            }
            catch (System.Exception ex)
            {

                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }

        }


        [HttpGet]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.DeleteAsync(uriBase + id.ToString());
                string serialized = await response.Content.ReadAsStringAsync();


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] =
                    string.Format("Livro Id {0}, removido com sucesso!", id);
                    return RedirectToAction("Index");

                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return RedirectToAction("Create");
            }
        }




    }
}