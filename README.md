# Convert CSV Files to JSON with Azure Functions

[Blob trigger function](https://docs.microsoft.com/azure/azure-functions/functions-bindings-storage-blob-trigger?WT.mc_id=academic-0000-jasmineg) that converts a CSV File to JSON file.

## Built with

- Visual Studio 2019 (with Azure development workload)
- VS Code and [Azure Functions Extension](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azurefunctions&WT.mc_id=academic-0000-jasmineg)
- [Azure Storage Explorer](https://azure.microsoft.com/features/storage-explorer/?WT.mc_id=academic-0000-jasmineg)
- [Azurite](https://github.com/azure/azurite)
- [Azure Functions Core Tools v3.0](https://docs.microsoft.com/azure/azure-functions/functions-run-local?WT.mc_id=academic-0000-jasmineg&tabs=windows,csharp,bash#install-the-azure-functions-core-tools)

## Running this sample locally

1. Clone/Download this repo and open in VS Code or Visual Studio
2. Install the tools above
3. Install and Start Azurite (startup varies with installation choice, refer to docs in [NPM](https://github.com/azure/azurite#npm), [Docker](https://github.com/azure/azurite#dockerhub), or [VS Code](https://github.com/azure/azurite#visual-studio-code-extension))
4. Open Azure Storage Explorer
5. Start from Local and attached directory **Storage Accounts** > **Emulator** > **Blob Containers**
6. Right click on **Blob containers** > **Create Blob Container**
7. Name container `to-convert`
8. Upload a csv file to the blob container, you can use the test file available [here](airport-test-data.csv).
9. Highlight the Emulator in and copy the **primary connection string** in **properties** at the bottom of the Explorer view.
10. Paste connection string into the value of `StorageConnString` in [the settings file](CsvToJSON/_local.settings.json)
11. Rename the file to remove the underscore: `local.settings.json`
12. Run code in:
    - Visual Studio > F5
    - VS Code > Run > Attach to .NET Functions > Start Debugging Button
    - Command line > `cd CSVToJSON` > `func start`
13. Want to deploy to [Azure](https://azure.microsoft.com/free/?WT.mc_id=academic-0000-jasmineg)? Refer to this [documentation on deployment options](https://docs.microsoft.com/azure/azure-functions/functions-deployment-technologies?WT.mc_id=academic-0000-jasmineg#deployment-methods)