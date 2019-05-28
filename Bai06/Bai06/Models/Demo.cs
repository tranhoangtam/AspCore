using System.Threading;
using System.Threading.Tasks;

namespace Bai06.Models
{
    public class Demo
    {
        public int Test01()
        {
            Thread.Sleep(5000);//5s
            return 7777;
        }
        public string Test02()
        {
            Thread.Sleep(2000);//2s
            return "Nhất Nghệ";
        }
        public void Test03()
        {
            Thread.Sleep(3000);//3s
        }

        public async Task<int> Test01Async()
        {
            await Task.Delay(5000);
            return 7777;
        }
        public async Task<string> Test02Async()
        {
            await Task.Delay(2000);
            return "Nhất Nghệ";
        }
        public async Task Test03Async()
        {
            await Task.Delay(3000);
        }

        public async Task SayGoogle()
        {
            //phải có lời gọi hàm
            return;
        }

    }
}
