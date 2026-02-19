using Microsoft.AspNetCore.Mvc;
using MvcCoreSession.Extensions;
using MvcCoreSession.Helpers;
using MvcCoreSession.Models;

namespace MvcCoreSession.Controllers
{
    public class EjemploSessionController : Controller
    {

        public IActionResult Index()
        {
            
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
                    ViewData["MASCOTA"] = mascota;

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

        //CREAMOS OTRO MÉTODO CON JSON
        public IActionResult SessionMascotaJSON(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    //GUARDAMOS DATOS EN SESSION
                    Mascota mascota = new Mascota
                    {
                        Nombre = "Spikesito",
                        Raza = "AZUL RUSO",
                        Edad = 5
                    };

                    //QUEREMOS GUARDAR EL OBJETO MASCOTA COMO STRING EN SESSION
                    string mascotaJson = HelperJsonSession.SerializeObject<Mascota>(mascota);

                    HttpContext.Session.SetString("MASCOTAJSON", mascotaJson);


                    ViewData["MENSAJE"] = "Mascota almacenada en session";

                }
                else if (accion.ToLower() == "mostrar")
                {
                    string mascotaJson = HttpContext.Session.GetString("MASCOTAJSON");
                    Mascota mascota = HelperJsonSession.DeserializableObject<Mascota>(mascotaJson);

                    ViewData["MASCOTA"] = mascota;

                }
            }
            return View();
        }

        //CONTROLADOR GENERICO
        public IActionResult SessionMascotaGenerico(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    //GUARDAMOS DATOS EN SESSION
                    Mascota mascota = new Mascota
                    {
                        Nombre = "Simbita",
                        Raza = "Bosque Noruego",
                        Edad = 2
                    };

                    HttpContext.Session.SetObject("MASCOTAGENERIC",mascota);

                    ViewData["MENSAJE"] = "Mascota almacenada en session";

                }
                else if (accion.ToLower() == "mostrar")
                {
                    Mascota mascota = HttpContext.Session.GetObject<Mascota>("MASCOTAGENERIC");

                    ViewData["MASCOTAGENERIC"] = mascota;

                }
            }
            return View();
        }

        //AHORA VAMOS A PROBAR CON LAS LISTAS
        public IActionResult SessionMascotasColectionGenerico(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    //GUARDAMOS DATOS EN SESSION

                    List<Mascota> mascotas = new List<Mascota>
                    {
                        new Mascota{Nombre="SPIKESITO",Raza="Bosque Noruego",Edad=3},
                        new Mascota{Nombre="Kronito",Raza="Perrito",Edad=7},
                        new Mascota{Nombre="Vacía",Raza="Periquito",Edad=2},
                        new Mascota{Nombre="Mili",Raza="Común",Edad=1},
                    };

                    HttpContext.Session.SetObject("MASCOTASGENERIC", mascotas);

                    ViewData["MENSAJE"] = "COLECCION DE MASCOTAS almacenada en session genéricamente";

                }
                else if (accion.ToLower() == "mostrar")
                {
                    List<Mascota> mascotas = HttpContext.Session.GetObject<List<Mascota>>("MASCOTASGENERIC");

                    //PARA REPRESENTARLO DE FORMA VISUAL LO ENVIAREMOS A LA VISTA
                    return View(mascotas);


                }
            }
            return View();
        }







    }
}
