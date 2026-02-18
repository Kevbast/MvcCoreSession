using Microsoft.AspNetCore.Mvc;
using MvcCoreSession.Helpers;
using MvcCoreSession.Models;

namespace MvcCoreSession.Controllers
{
    public class EjemploSessionController : Controller
    {

        public IActionResult Index()
        {
            if (HttpContext.Session.Get("MASCOTA") != null) { 
            Mascota infomascota =(Mascota) TempData["MASCOTA"];
            //Console.Write(infomascota);
            byte[] data = HttpContext.Session.Get("MASCOTA");
            //CONVERTIMOS A BYTES A OBJECT
            Mascota mascota = (Mascota)HelperBinarySession.ByteToObject(data);
            return View(mascota);
            }
            return View();
        }


        public IActionResult SessionSimple(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    //GUARDAMOS DATOS EN SESSION
                    HttpContext.Session.SetString("nombre", "Programeitor");
                    HttpContext.Session.SetString("hora", DateTime.Now.ToLongTimeString());
                    ViewData["MENSAJE"]="DATOS ALMACENADOS EN SESSION!!";
                }else if (accion.ToLower() == "mostrar")
                {
                    //RECUPERAMOS LOS DATOS DE SESSION
                    ViewData["NOMBRE"]=HttpContext.Session.GetString("nombre");
                    ViewData["HORA"]=HttpContext.Session.GetString("hora");


                }
            }


            return View();
        }

        public IActionResult SessionMascotaBytes(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    //GUARDAMOS DATOS EN SESSION
                    Mascota mascota = new Mascota
                    {
                        Nombre = "Spike",
                        Raza = "Azul",
                        Edad = 5
                    };
                    //PARA ALMACENAR LA MASCOTA EM Session debemos convertirlo a byte[]
                    byte[] data = HelperBinarySession.ObjectToByte(mascota);
                    //almacenamos el objeto en session
                    HttpContext.Session.Set("MASCOTA", data);
                    ViewData["MENSAJE"] = "Mascota almacenada en session";

                }
                else if (accion.ToLower() == "mostrar")
                {
                    //RECUPERAMOS LOS DATOS DE SESSION
                    //Recuperamos los datos de mascota en bytes que tenemos en session
                    byte[] data = HttpContext.Session.Get("MASCOTA");
                    //CONVERTIMOS A BYTES A OBJECT
                    Mascota mascota = (Mascota)HelperBinarySession.ByteToObject(data);

                    //PARA REPRESENTARLO DE FORMA VISUAL LO ENVIAMOS A VIEWDATA
                    TempData["MASCOTA"] = mascota;
                    //return RedirectToAction("Index");

                }
            }
            return View();
        }

        //EJEMPLO PERO CON LISTA DE MASCOTAS

        public IActionResult SessionMascotasColectionBytes(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    //GUARDAMOS DATOS EN SESSION

                    List<Mascota> mascotas = new List<Mascota>
                    {
                        new Mascota{Nombre="Simba",Raza="Bosque Noruego",Edad=3},
                        new Mascota{Nombre="Vacía",Raza="Periquito",Edad=2},
                        new Mascota{Nombre="Krono",Raza="Perrito",Edad=6},
                        new Mascota{Nombre="Mili",Raza="Común",Edad=1},
                    };

                    byte[] data = HelperBinarySession.ObjectToByte(mascotas);
                    HttpContext.Session.Set("MASCOTAS", data);


                    ViewData["MENSAJE"] = "COLECCION DE MASCOTAS almacenada en session";

                }
                else if (accion.ToLower() == "mostrar")
                {
                    byte[] data = HttpContext.Session.Get("MASCOTAS");
                    //CONVERTIMOS A BYTES A OBJECT
                    List<Mascota> mascotas = (List<Mascota>)HelperBinarySession.ByteToObject(data);

                    //PARA REPRESENTARLO DE FORMA VISUAL LO ENVIAREMOS A LA VISTA
                    return View(mascotas);


                }
            }
            return View();
        }




    }
}
