using System;
using System.Collections.ObjectModel;
using Moq;
using Prism.Modularity;
using Xunit;

namespace Prism.Avalonia.Tests.Modularity
{
    public class FileModuleTypeLoaderFixture
    {
        [Fact]
        public void CanRetrieveModule()
        {
            var assemblyResolver = new MockAssemblyResolver();
            var retriever = new FileModuleTypeLoader(assemblyResolver);
            string assembly = CompilerHelper.GenerateDynamicModule("FileModuleA", null);
            string assemblyRef = "file://" + assembly;
            var fileModuleInfo = CreateModuleInfo(assemblyRef, "TestModules.FileModuleAClass", "ModuleA", true, null);

            bool loadCompleted = false;
            retriever.LoadModuleCompleted += delegate (object sender, LoadModuleCompletedEventArgs e)
            {
                loadCompleted = true;
            };

            retriever.LoadModuleType(fileModuleInfo);

            Assert.True(loadCompleted);
            Assert.Equal(assemblyRef, assemblyResolver.LoadAssemblyFromArgument);
        }

        [Fact]
        public void ShouldReturnErrorToCallback()
        {
            var assemblyResolver = new MockAssemblyResolver();
            var retriever = new FileModuleTypeLoader(assemblyResolver);
            var fileModuleInfo = CreateModuleInfo("NonExistentFile.dll", "NonExistentModule", "NonExistent", true, null);

            assemblyResolver.ThrowOnLoadAssemblyFrom = true;
            Exception resultException = null;

            bool loadCompleted = false;
            retriever.LoadModuleCompleted += delegate (object sender, LoadModuleCompletedEventArgs e)
            {
                loadCompleted = true;
                resultException = e.Error;
            };

            retriever.LoadModuleType(fileModuleInfo);

            Assert.True(loadCompleted);
            Assert.NotNull(resultException);
        }

        [Fact]
        public void CanRetrieveWithCorrectRef()
        {
            var retriever = new FileModuleTypeLoader();
            var moduleInfo = new ModuleInfo() { Ref = "file://somefile" };

            Assert.True(retriever.CanLoadModuleType(moduleInfo));
        }

        [Fact]
        public void CannotRetrieveWithIncorrectRef()
        {
            var retriever = new FileModuleTypeLoader();
            var moduleInfo = new ModuleInfo() { Ref = "NotForLocalRetrieval" };

            Assert.False(retriever.CanLoadModuleType(moduleInfo));
        }

        [Fact]
        public void FileModuleTypeLoaderCanBeDisposed()
        {
            var typeLoader = new FileModuleTypeLoader();
            var disposable = typeLoader as IDisposable;

            Assert.NotNull(disposable);
        }

        [Fact]
        public void FileModuleTypeLoaderDisposeNukesAssemblyResolver()
        {
            Mock<IAssemblyResolver> mockResolver = new Mock<IAssemblyResolver>();
            var disposableMockResolver = mockResolver.As<IDisposable>();
            disposableMockResolver.Setup(resolver => resolver.Dispose());

            var typeLoader = new FileModuleTypeLoader(mockResolver.Object);

            typeLoader.Dispose();

            disposableMockResolver.Verify(resolver => resolver.Dispose(), Times.Once());
        }

        [Fact]
        public void FileModuleTypeLoaderDisposeDoesNotThrowWithNonDisposableAssemblyResolver()
        {
            Mock<IAssemblyResolver> mockResolver = new Mock<IAssemblyResolver>();
            var typeLoader = new FileModuleTypeLoader(mockResolver.Object);
            try
            {
                typeLoader.Dispose();
            }
            catch (Exception)
            {
                //Assert.Fail();
            }
        }

        private static ModuleInfo CreateModuleInfo(string assemblyFile, string moduleType, string moduleName, bool startupLoaded, params string[] dependsOn)
        {
            ModuleInfo moduleInfo = new ModuleInfo(moduleName, moduleType)
            {
                InitializationMode = startupLoaded ? InitializationMode.WhenAvailable : InitializationMode.OnDemand,
                Ref = assemblyFile,
            };
            if (dependsOn != null)
            {
                moduleInfo.DependsOn.AddRange(dependsOn);
            }

            return moduleInfo;
        }
    }

    internal class MockAssemblyResolver : IAssemblyResolver
    {
        public string LoadAssemblyFromArgument;
        public bool ThrowOnLoadAssemblyFrom;

        public void LoadAssemblyFrom(string assemblyFilePath)
        {
            LoadAssemblyFromArgument = assemblyFilePath;
            if (ThrowOnLoadAssemblyFrom)
                throw new Exception();
        }
    }
}
