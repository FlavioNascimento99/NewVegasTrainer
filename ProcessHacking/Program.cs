using System;                           // Traz classes fundamentais do .NET
using System.ComponentModel;
using System.Diagnostics;               // Traz namespace que carrega cconsigo a possibilidade de interação com outros processos dentro do sistema.
using System.Runtime.InteropServices;   // Traz a interação com as APIs do Windows.

class ProcessHacking
{
    // DllImport tem como funcionalidade avisar que a função a seguir faz parte da biblioteca do Windows, kernel32.
    [DllImport("kernel32.dll")]

    // OpenProcess:
    // Nível de Acesso,
    // Alvo Alterável?,
    // Id do Processo;
    private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]

    // ReadProcessMemory:
    // Alvo de Ateração,
    // Endereço de Leitura,
    // Localização dos dados temporários (Buffer),
    // Tamanho dos dados,
    // Numero de Bytes a serem lidos;
    public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]

    // WriteProcess:
    // Alvo da Alteração,
    // Endereço a ser escrito,
    // Memória temporária com os dados alteráveis,
    // Tamanho dos dados a serem escritos,
    // Numero de Bytes que foram escritos;
    public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);

    [DllImport("kernel32.dll")]
    // CloseHandle:
    // Fecha a leitura de processo;
    public static extern bool CloseHandle(IntPtr hObject);


    // Constantes de Acesso:
    // READ: PERMISSÃO PARA LEITURA
    // WRITE: ESCRITA
    // OPERATION: OPERAÇÕES EM MEMÓRIA DE DETERMINADO PROCESSO.
    const int PROCESS_WM_READ = 0x0010;
    const int PROCESS_WM_WRITE = 0x0020;
    const int PROCESS_WM_OPERATION = 0x0008;


    // Função Principal.
    static void Main(string[] args)
    {

        // Nome do Processo alvo, Fallout New Vegas.
        string ProcessName = "FalloutNV";

        // Endereço de Acesso ao dado que deverá ser alterado (To-Do)
        IntPtr BaseAddress = new IntPtr(0x0D34894C); 

        // Busca todos os processos com nome idêntico ao da variuável ProcessName, e captura o primeiro que for encontrado.
        Process process = Process.GetProcessesByName(ProcessName)[0];

        // Após encontrado o processo, o abre com as devidas permissões, sem herança de handles e por fim, localizando o Id do Porcesso.
        IntPtr ProcessHandle = OpenProcess(PROCESS_WM_READ | PROCESS_WM_WRITE | PROCESS_WM_OPERATION, false, process.Id);

        // Condicional que será executada caso não seja possivel encontrar o processo.
        if (ProcessHandle == IntPtr.Zero)
        {
            Console.WriteLine("Não foi possível encontrar o processo :/");
            return;
        }

        // Criamos um buffer de 4 bytes, afinal o valor esperado para passarmos é de um inteiro.
        byte[] buffer = new byte[4];
        int bytesRead = 0;

        // 
        ReadProcessMemory(ProcessHandle, BaseAddress, buffer, buffer.Length, out bytesRead);
        
        int CurrentValue = BitConverter.ToInt32(buffer, 0);
        Console.WriteLine("O Valor atual de Caps é de: " +  CurrentValue);


        int newValue = 99999;
        byte[] newBuffer = BitConverter.GetBytes(newValue);
        int bytesWritten;
        WriteProcessMemory(ProcessHandle, BaseAddress, newBuffer, newBuffer.Length, out bytesWritten);
        Console.WriteLine("Valor alterado para: " + newValue);

        CloseHandle(ProcessHandle); 
    }
}