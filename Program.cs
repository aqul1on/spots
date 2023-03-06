namespace WinFormsApp1;

static class Program
{
   
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
                
        Form1 form1 = new Form1();
        Application.Run(form1);
    }
}