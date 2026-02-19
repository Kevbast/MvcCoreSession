using MvcCoreSession.Helpers;

namespace MvcCoreSession.Extensions
{
    public static class SessionExtension
    {
        //METODO PARA RECUPERAR CUALQUIER OBJETO DE SESSION
        public static T GetObject<T>
            (this ISession session,string key)
        {
            //AHORA MISMO YA TENEMOS DENTRO DE LA VARIABLE SESSION EL OBJETO HTTPContext.Session
            //Debemos recuperar el objeto json de session
            string json = session.GetString(key);

            if (json == null)
            {//SE DEVUELVE EL VALOR POR DEFECTO DE T
                return default(T);
            }
            else
            {
                //RECUPERAMOS EL OBJETO Y LO CONVERTIMOS CON NUESTRO HELPER
                T data = HelperJsonSession.DeserializableObject<T>(json);
                return data;
            }


        }


        public static void SetObject(this ISession session,string key,object value)// se necesita la key y cualquier objeto
        {
            string data = HelperJsonSession.SerializeObject(value);
            session.SetString(key, data);

        }


    }
}
