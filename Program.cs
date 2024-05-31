
//using GemBox.Email;
//using GemBox.Email.Imap;
using Aspose.Email.Clients.Imap;
using System.Net.Mail;
using System.Text;

namespace ask1
{
    internal class Program
    {
        public static void Save(string data)
        {
            string path = @"C:\Users\Пользователь\source\repos\ask1\Data\data.txt";
            File.AppendAllText(path, data + "\n");
        }

        public static bool Find(string curMail)
        {
            string viewedMail;
            using (var f = new StreamReader(@"C:\Users\Пользователь\source\repos\ask1\Data\data.txt"))
            {
                while ((viewedMail = f.ReadLine()) != null)
                {
                    if (curMail == viewedMail)
                        return false;
                }
            }
            return true;
        }
        static void Main(string[] args)
        {
            using (var client = new ImapClient("imap.mail.ru", "ziliyamustafina06@mail.ru", "1GWa6vsewxH3u3Gz99xr"))
            {
                client.SelectFolder(ImapFolderInfo.InBox);
                var mailList = client.ListMessages();
                Thread thread = new Thread(
                    delegate ()
                    {
                        foreach (var mail in mailList)
                        {
                            if (mail.Date > DateTime.Now.AddMonths(-1))
                            {
                                if (Find(mail.UniqueId))
                                {
                                    Save(mail.UniqueId);
                                    Console.WriteLine(mail.From.DisplayName + ", " + mail.Subject);
                                }
                            }
                        }
                    }
                );
                thread.Start();
                client.Dispose();
            }
        }
    }
}
