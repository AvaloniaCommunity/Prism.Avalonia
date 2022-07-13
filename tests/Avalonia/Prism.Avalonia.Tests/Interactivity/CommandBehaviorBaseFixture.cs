using System.Windows.Input;
using Avalonia.Controls;
using Prism.Interactivity;
using Xunit;

namespace Prism.Avalonia.Tests.Interactivity
{

    public class CommandBehaviorBaseFixture
    {
        [Fact]
        public void ExecuteUsesCommandParameterWhenSet()
        {
            var targetUIElement = new Control();
            var target = new TestableCommandBehaviorBase(targetUIElement);
            target.CommandParameter = "123";
            TestCommand testCommand = new TestCommand();
            target.Command = testCommand;

            target.ExecuteCommand("testparam");

            Assert.Equal("123", testCommand.ExecuteCalledWithParameter);
        }

        [Fact]
        public void ExecuteUsesParameterWhenCommandParameterNotSet()
        {
            var targetUIElement = new Control();
            var target = new TestableCommandBehaviorBase(targetUIElement);
            TestCommand testCommand = new TestCommand();
            target.Command = testCommand;

            target.ExecuteCommand("testparam");

            Assert.Equal("testparam", testCommand.ExecuteCalledWithParameter);
        }

        [Fact]
        public void CommandBehaviorBaseAllowsDisableByDefault()
        {
            var targetUIElement = new Control();
            var target = new TestableCommandBehaviorBase(targetUIElement);

            Assert.True(target.AutoEnable);
        }

        [StaFact]
        public void CommandBehaviorBaseEnablesUIElement()
        {
            var targetUIElement = new Control();
            targetUIElement.IsEnabled = false;

            var target = new TestableCommandBehaviorBase(targetUIElement);
            TestCommand testCommand = new TestCommand();
            target.Command = testCommand;
            target.ExecuteCommand(null);

            Assert.True(targetUIElement.IsEnabled);
        }

        [StaFact]
        public void CommandBehaviorBaseDisablesUIElement()
        {
            var targetUIElement = new Control();
            targetUIElement.IsEnabled = true;

            var target = new TestableCommandBehaviorBase(targetUIElement);
            TestCommand testCommand = new TestCommand();
            testCommand.CanExecuteResult = false;
            target.Command = testCommand;
            target.ExecuteCommand(null);

            Assert.False(targetUIElement.IsEnabled);
        }

        [StaFact]
        public void WhenAutoEnableIsFalse_ThenDisabledUIElementRemainsDisabled()
        {
            var targetUIElement = new Control();
            targetUIElement.IsEnabled = false;

            var target = new TestableCommandBehaviorBase(targetUIElement);
            target.AutoEnable = false;
            TestCommand testCommand = new TestCommand();
            target.Command = testCommand;
            target.ExecuteCommand(null);

            Assert.False(targetUIElement.IsEnabled);
        }

        [StaFact]
        public void WhenAutoEnableIsUpdated_ThenDisabledUIElementIsEnabled()
        {
            var targetUIElement = new Control();
            targetUIElement.IsEnabled = false;

            var target = new TestableCommandBehaviorBase(targetUIElement);
            target.AutoEnable = false;
            TestCommand testCommand = new TestCommand();
            target.Command = testCommand;
            target.ExecuteCommand(null);

            Assert.False(targetUIElement.IsEnabled);

            target.AutoEnable = true;

            Assert.True(targetUIElement.IsEnabled);
        }

        [StaFact]
        public void WhenAutoEnableIsUpdated_ThenEnabledUIElementIsDisabled()
        {
            var targetUIElement = new Control();
            targetUIElement.IsEnabled = true;

            var target = new TestableCommandBehaviorBase(targetUIElement);
            target.AutoEnable = false;
            TestCommand testCommand = new TestCommand();
            testCommand.CanExecuteResult = false;
            target.Command = testCommand;
            target.ExecuteCommand(null);

            Assert.True(targetUIElement.IsEnabled);

            target.AutoEnable = true;

            Assert.False(targetUIElement.IsEnabled);
        }
    }

    class TestableCommandBehaviorBase : CommandBehaviorBase<Control>
    {
        public TestableCommandBehaviorBase(Control targetObject)
            : base(targetObject)
        { }

        public new void ExecuteCommand(object parameter)
        {
            base.ExecuteCommand(parameter);
        }
    }

    class TestCommand : ICommand
    {
        bool _canExecte = true;
        public bool CanExecuteResult
        {
            get { return _canExecte; }
            set { _canExecte = value; }
        }

        public object CanExecuteCalledWithParameter { get; set; }

        public bool CanExecute(object parameter)
        {
            CanExecuteCalledWithParameter = parameter;
            return CanExecuteResult;
        }

        public event EventHandler CanExecuteChanged;

        public object ExecuteCalledWithParameter { get; set; }
        public void Execute(object parameter)
        {
            ExecuteCalledWithParameter = parameter;
        }
    }

}
