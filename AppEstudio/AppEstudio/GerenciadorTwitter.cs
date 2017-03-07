using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;


using Tweetinvi;

namespace AppEstudio
{
    class GerenciadorTwitter
    {

        string resultado = "";
        Thread t;

        public static string TAG = "#bbb";

        public GerenciadorTwitter()
        {
            //Tweetinvi Tutorial - Introduction to Twitter in C#
            //www.youtube.com/watch?v=1maeTudF8cQ
            //Encontre em: apps.twitter.com
            Auth.SetUserCredentials("Y35l7y7Z6HN1E8viG9LU2P4PB", "8pKZ3ttYe02vyKCQeJUSxfTorVIXGv9QIXAlI6oLikc5Bx0ake",
                "2509227109-Sy3UQS2AfATSsdJevQxbWradahTRJuqWZp9pxQO", "Ab1yaNzSHEGUSDOHAar13aPi34Wwtn01JJcyFwWqvBEEs");          
        }

        void Procurar()
        {
            try
            {
                var tweets = Search.SearchTweets(TAG);
                //***Nenhum tweet começa com espaço***
                //***Usar espaço para diferenciar user/text***
                var user = User.GetAuthenticatedUser();
                Array a = tweets.ToArray();
                for (int i = 0; i < a.Length; i++)
                {
                    if (!tweets.ElementAt(i).IsRetweet)
                    {
                        resultado = tweets.ElementAt(i).CreatedBy.ScreenName + " " + tweets.ElementAt(i).FullText;
                        i = a.Length;
                    }
                }
            }
            catch
            {
            }
            
        }

        public string Tweet()
        {
            Procurar();
            return resultado;
        }
    }
}
