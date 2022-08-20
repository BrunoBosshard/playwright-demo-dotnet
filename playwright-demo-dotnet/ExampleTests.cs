using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace playwright_demo_net;

[Parallelizable(ParallelScope.Self)]
public class ExampleTests : PageTest
{
    [SetUp]
    public void Setup()
    {
    }

    public override BrowserNewContextOptions ContextOptions()
    {
        var options = base.ContextOptions() ?? new();
        options.RecordVideoDir = "videos";

        return options;
    }

    [Test]
    public async Task BasicTest()
    {
        await Page.GotoAsync("https://playwright.dev");
        // Alternative via ".runsettings" file
        // await Page.GotoAsync(TestContext.Parameters["PlaywrightHomepage"]);

        // Create a locator
        var title = Page.Locator(".navbar__inner .navbar__title");

        // Expect title to to have text "Playwright"
        await Expect(title).ToHaveTextAsync("Playwright");
    }

    // Data-Driven Test
    [Test]
    public async Task CheckUrls()
    {
        string[] urls = File.ReadAllLines("urls.txt");
        foreach (string url in urls)
        {
            Console.WriteLine(url);
            await Page.GotoAsync(url);
        }
    }

    [TearDown]
    public async Task TearDown()
    {
        if (Page?.Video != null)
            TestContext.AddTestAttachment(await Page.Video.PathAsync());
    }

}