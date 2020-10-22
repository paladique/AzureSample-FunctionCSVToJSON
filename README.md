# Convert CSV Files to JSON with Azure Functions

Uses a container

## Built with

- Visual Studio 2019 (with Azure development workload), documented with VS Code
- [Azure Storage Explorer](https://azure.microsoft.com/en-us/features/storage-explorer/)
- [Azurite](https://github.com/azure/azurite)

## Running this sample locally

1. Clone/Download this repo and open in VS Code or Visual Studio
2. Install and Start Azurite (startup varies with installation choice, refer to docs in [NPM](https://github.com/azure/azurite#npm), [Docker](https://github.com/azure/azurite#dockerhub), or [VS Code](https://github.com/azure/azurite#visual-studio-code-extension))
3. Open Azure Storage Explorer
4. Start from Local and attached directory > Emulator > Blob Containers
5. Right click on Blob containers > Create Blob Container
6. Name container `to-convert`