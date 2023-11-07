using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FooController : ControllerBase
    {
        [HttpGet(Name = "GetFoo")]
        public async Task<IActionResult> Get()
        {
            Console.WriteLine(Environment.CurrentManagedThreadId);

            var result = await Task.FromResult("fooooo");
            return Ok(result);
        }


        [HttpGet("stream", Name = "StreamSync")]

        public Task<IEnumerable<int>> SyncStreamOldSchool()
        {
            var result = SyncStreamOldSchoolInts();
            return Task.FromResult(result);
        }

        [HttpGet("async-stream", Name = "StreamAsync")]

        public ActionResult<IAsyncEnumerable<int>> StreamAsync()
        {
            var result = AsyncStreamInts();
            return Ok(result);
        }

        private static IEnumerable<int> SyncStreamOldSchoolInts()
        {
            foreach (var item in RangeEnumerable(1, 100))
            {
                yield return item;
            }
        }

        private static async IAsyncEnumerable<int> AsyncStreamInts()
        {
            await foreach (var item in Range(1, 100))
            {
                yield return item;
            }
        }

        static async IAsyncEnumerable<int> Range(int start, int count)
        {
            for (int i = 0; i < count; i++)
            {
                await Task.Delay(100);
                yield return start + i;
            }
        }

        static IEnumerable<int> RangeEnumerable(int start, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Thread.Sleep(100);
                yield return start + i;
            }
        }
    }
}
