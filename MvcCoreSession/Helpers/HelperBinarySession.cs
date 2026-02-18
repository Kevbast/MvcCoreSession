using System.Runtime.Serialization.Formatters.Binary;

namespace MvcCoreSession.Helpers
{

    public class HelperBinarySession
    {

        //VAMOS A CREAR LOS MÉTODOS DE TIPO STATIC
        //PORQUE PARA CONVERTIR NO VOY A UTILIZAR NADA DE ESTA CLASE
        //SOLO LA FUNCIONALIDAD
        //CONVERTIMOS OBJETO A BYTE[]
        public static byte[] ObjectToByte(Object objeto)
        {

            BinaryFormatter formatter = new BinaryFormatter();
            //LO CONVERTIMOS A BINARIO
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, objeto);
                return stream.ToArray();
            }
            

        }
        //CONVERTIMOS DE BYTE[] A OBJETO
        public static Object ByteToObject(byte[]data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            //LO CONVERTIMOS A BINARIO
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Seek(0, SeekOrigin.Begin);
                Object objeto = (Object)
                    formatter.Deserialize(stream);
                return objeto;
            }


        }

    }
}
