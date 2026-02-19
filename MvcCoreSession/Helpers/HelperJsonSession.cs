using Newtonsoft.Json;

namespace MvcCoreSession.Helpers
{
    public class HelperJsonSession
    {
        //Vamos a utilizar string mediante JSON el nugget newtonsoft.JSON
        //VAMOS A ALMACENAR DATOS EN SESSION MEDIANTE EL METODO GetString,SetString
        public static string SerializeObject<T>(T data)
        {//ESTE MÉTODO ES GENERICO,POR ESO USAMOS <T>
            //CONVERTIMOS EL OBJETO A STRING MEDIANTE NEWTON
            string json = JsonConvert.SerializeObject(data);
            return json;
        } 

        //RECIBIMOS UN STRING Y DEVOLVER CUALQUIER OBJETO
        public static T DeserializableObject<T>(string data)
        {
            //MEDIANTE NEWTON DESERIALIZAMOS EL OBJETO
            T objeto = JsonConvert.DeserializeObject<T>(data);
            return objeto;
        }

    }
}
