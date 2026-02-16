namespace JazzcashPortal.Models
{
    public class ClsLog
    {
        public void WriteToFile(string Message, List<object>? Response)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    if (Response != null)
                    {
                        sw.WriteLine(Message + Response[0]);
                    }
                    else
                    {
                        sw.WriteLine(Message);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 1000; i++)
                {
                    try
                    {
                        using (StreamWriter sw = File.AppendText(filepath))
                        {
                            if (Response != null)
                            {
                                sw.WriteLine(Message + Response[0]);
                            }
                            else
                            {
                                sw.WriteLine(Message);
                            }

                        }
                        break;
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
    }
}
