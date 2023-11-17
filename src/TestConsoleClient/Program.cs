using System;

using Facebook;


var facebookClient = new FacebookApi("shit");

try
{
    var meInfo = await facebookClient.GetAsync("me");
    Console.WriteLine(meInfo);
}
catch (Exception e)
{
    Console.WriteLine(e);
}