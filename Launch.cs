using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plasmic_Client
{
    public partial class Control : UserControl
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
            uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess,
            IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        // privileges
        const int PROCESS_CREATE_THREAD = 0x0002;
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int PROCESS_VM_OPERATION = 0x0008;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_READ = 0x0010;

        // used for memory allocation
        const uint MEM_COMMIT = 0x00001000;
        const uint MEM_RESERVE = 0x00002000;
        const uint PAGE_READWRITE = 4;

        static bool alreadyAttemptedInject = false;



        public Control()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Process.Start("Minecraft://");
        }

        private void Control_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {

        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            Process.Start("Minecraft://");
            //Interaction.Shell();

            Logger.Log("Gaming Launched!!!");
            button1.Text = "Injecting DLL";
            await Task.Delay(2500);
            button1.Text = "Injected";

            OpenFileDialog FileIn = new OpenFileDialog();
            if (FileIn.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (FileIn.SafeFileName.ToLower().EndsWith(".dll"))
                {
                    InjectDLL(FileIn.FileName);
                    Process.Start("Minecraft://");
                    //Interaction.Shell();
                    InjectDLL(FileIn.FileName);
                    Logger.Log("Gaming Launched!!!");
                    button1.Text = "Injecting DLL";
                    await Task.Delay(2500);
                    button1.Text = "Injected";
                }
                else
                {
                    MessageBox.Show("You did not specify a DLL!");
                }
            }
            

                
           }
        public static void InjectDLL(string DLLPath)
        {
            Process[] targetProcessIndex = Process.GetProcessesByName("Minecraft.Windows");
            if (targetProcessIndex.Length > 0)
            {
                applyAppPackages(DLLPath);

                Process targetProcess = Process.GetProcessesByName("Minecraft.Windows")[0];
                IntPtr procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, targetProcess.Id);

                IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

                IntPtr allocMemAddress = VirtualAllocEx(procHandle, IntPtr.Zero, (uint)((DLLPath.Length + 1) * Marshal.SizeOf(typeof(char))), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);

                UIntPtr bytesWritten;
                WriteProcessMemory(procHandle, allocMemAddress, Encoding.Default.GetBytes(DLLPath), (uint)((DLLPath.Length + 1) * Marshal.SizeOf(typeof(char))), out bytesWritten);
                CreateRemoteThread(procHandle, IntPtr.Zero, 0, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);

                alreadyAttemptedInject = false;
                MessageBox.Show("Injected!");
            }
            else
            {
                if (!alreadyAttemptedInject)
                {
                    alreadyAttemptedInject = true;
                    MessageBox.Show("Minecraft is not open! Launching Now...");
                    Process.Start("minecraft://");
                    Thread.Sleep(2000);
                    InjectDLL(DLLPath);
                }
                else
                {
                    MessageBox.Show("You do not appear to have Minecraft installed!");
                    alreadyAttemptedInject = false;
                }
            }
        }



        public static void applyAppPackages(string DLLPath)
        {
            FileInfo InfoFile = new FileInfo(DLLPath);
            FileSecurity fSecurity = InfoFile.GetAccessControl();
            fSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier("S-1-15-2-1"), FileSystemRights.FullControl, InheritanceFlags.None, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            InfoFile.SetAccessControl(fSecurity);
        }

        private void LogsRefresher_Tick(object sender, EventArgs e)
        {
            label1.Text = System.IO.File.ReadAllText("Workspace/Logs.txt");
        }
    }
}

  
    
    


