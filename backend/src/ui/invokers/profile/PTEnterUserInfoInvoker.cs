using System;
using static NutriApp.UI.ParserUtils;

namespace NutriApp.UI;

class PTEnterUserInfoInvoker : CommandInvoker<(User, double, double)>
{
    private App app;

    public PTEnterUserInfoInvoker(Command<(User, double, double)> command, App app) : base(command)
    {
        this.app = app;
    }

    public override void Invoke()
    {
        var name = GetInputFromConsole<string>("Please enter your name");
        var height = GetInputFromConsole<int>("Please enter your height in inches");
        var birthday = GetInputFromConsole<DateTime>("Please enter your birthday");
        var weight = GetInputFromConsole<double>("Please enter your weight");
        var targetWeight = GetInputFromConsole<double>("Please enter the weight you'd like to achieve");

       // var user = new User(name, height, birthday);
        
       // command.Execute((user, weight, targetWeight));
    }
}