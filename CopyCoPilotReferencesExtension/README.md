<a name='assembly'></a>
# CopyCoPilotReferencesExtension

## Contents

- [CopyCoPilotReferencesCommand](#T-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand')
  - [#ctor(package,commandService)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-#ctor-Microsoft-VisualStudio-Shell-AsyncPackage,System-ComponentModel-Design-IMenuCommandService- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.#ctor(Microsoft.VisualStudio.Shell.AsyncPackage,System.ComponentModel.Design.IMenuCommandService)')
  - [CommandId](#F-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-CommandId 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.CommandId')
  - [CommandSet](#F-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-CommandSet 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.CommandSet')
  - [_commandService](#F-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-_commandService 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand._commandService')
  - [_package](#F-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-_package 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand._package')
  - [Instance](#P-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Instance 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.Instance')
  - [#cctor()](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-#cctor 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.#cctor')
  - [EndWaitDialog(dialog)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-EndWaitDialog-Microsoft-VisualStudio-Shell-Interop-IVsThreadedWaitDialog2- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.EndWaitDialog(Microsoft.VisualStudio.Shell.Interop.IVsThreadedWaitDialog2)')
  - [Execute()](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Execute-System-Object,System-EventArgs- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.Execute(System.Object,System.EventArgs)')
  - [GenerateCopilotReferences(projectItems,solutionDirectory)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-GenerateCopilotReferences-System-Collections-Generic-IReadOnlyList{EnvDTE-ProjectItem},System-String- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.GenerateCopilotReferences(System.Collections.Generic.IReadOnlyList{EnvDTE.ProjectItem},System.String)')
  - [GetSelectedProjectItems(dte2)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-GetSelectedProjectItems-EnvDTE80-DTE2- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.GetSelectedProjectItems(EnvDTE80.DTE2)')
  - [GetSolutionDirectory(dte2)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-GetSolutionDirectory-EnvDTE80-DTE2- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.GetSolutionDirectory(EnvDTE80.DTE2)')
  - [InitializeAsync(package)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-InitializeAsync-Microsoft-VisualStudio-Shell-AsyncPackage- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.InitializeAsync(Microsoft.VisualStudio.Shell.AsyncPackage)')
  - [JoinReferences(tokens)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-JoinReferences-System-Collections-Generic-IReadOnlyList{System-String}- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.JoinReferences(System.Collections.Generic.IReadOnlyList{System.String})')
  - [NormalizeSeparators()](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-NormalizeSeparators-System-String- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.NormalizeSeparators(System.String)')
  - [OnBeforeQueryStatus(sender,e)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-OnBeforeQueryStatus-System-Object,System-EventArgs- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.OnBeforeQueryStatus(System.Object,System.EventArgs)')
  - [StartWaitDialog(caption,message,status)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-StartWaitDialog-System-String,System-String,System-String- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.StartWaitDialog(System.String,System.String,System.String)')
  - [TryCopyToClipboard(text)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-TryCopyToClipboard-System-String- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.TryCopyToClipboard(System.String)')
- [CopyCoPilotReferencesPackage](#T-CopyCoPilotReferencesExtension-CopyCoPilotReferencesPackage 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesPackage')
  - [#ctor()](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesPackage-#ctor 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesPackage.#ctor')
  - [PackageGuidString](#F-CopyCoPilotReferencesExtension-CopyCoPilotReferencesPackage-PackageGuidString 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesPackage.PackageGuidString')
  - [#cctor()](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesPackage-#cctor 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesPackage.#cctor')
  - [InitializeAsync(cancellationToken,progress)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesPackage-InitializeAsync-System-Threading-CancellationToken,System-IProgress{Microsoft-VisualStudio-Shell-ServiceProgressData}- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesPackage.InitializeAsync(System.Threading.CancellationToken,System.IProgress{Microsoft.VisualStudio.Shell.ServiceProgressData})')
- [Get](#T-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Get 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.Get')
  - [LOG_FILE_PATH_TERMINATOR](#F-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Get-LOG_FILE_PATH_TERMINATOR 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.Get.LOG_FILE_PATH_TERMINATOR')
  - [AssemblyCompany](#P-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Get-AssemblyCompany 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.Get.AssemblyCompany')
  - [AssemblyProduct](#P-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Get-AssemblyProduct 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.Get.AssemblyProduct')
  - [AssemblyTitle](#P-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Get-AssemblyTitle 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.Get.AssemblyTitle')
  - [ApplicationProductName()](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Get-ApplicationProductName 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.Get.ApplicationProductName')
  - [LogFilePath()](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Get-LogFilePath 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.Get.LogFilePath')

<a name='T-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand'></a>
## CopyCoPilotReferencesCommand `type`

##### Namespace

CopyCoPilotReferencesExtension

##### Summary

Defines the publicly-exposed events, methods and properties of a Visual
Studio extension command that copies selected file(s) from Solution
Explorer to the clipboard in GitHub Copilot reference format.

##### Remarks

This class implements a singleton pattern and registers itself as a menu
command within Visual Studio.



The command retrieves selected file(s) from Solution Explorer, converts
their path(s) to solution-relative path(s), and formats them as
`#file:'relativePath'` reference(s) for use with GitHub Copilot.



If invalid value(s) are encountered during execution (such as
`null`
DTE service or empty solution), the command will fail
gracefully without copying anything to the clipboard.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-#ctor-Microsoft-VisualStudio-Shell-AsyncPackage,System-ComponentModel-Design-IMenuCommandService-'></a>
### #ctor(package,commandService) `constructor`

##### Summary

Constructs a new instance of
[CopyCoPilotReferencesCommand](#T-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand')
and returns a reference to it.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| package | [Microsoft.VisualStudio.Shell.AsyncPackage](#T-Microsoft-VisualStudio-Shell-AsyncPackage 'Microsoft.VisualStudio.Shell.AsyncPackage') | (Required.) Reference to an instance of
[AsyncPackage](#T-Microsoft-VisualStudio-Shell-AsyncPackage 'Microsoft.VisualStudio.Shell.AsyncPackage')
that owns this
command. |
| commandService | [System.ComponentModel.Design.IMenuCommandService](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ComponentModel.Design.IMenuCommandService 'System.ComponentModel.Design.IMenuCommandService') | (Required.) Reference to an instance of
[IMenuCommandService](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ComponentModel.Design.IMenuCommandService 'System.ComponentModel.Design.IMenuCommandService')
that is
used to register this command. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Thrown if either `package` or
`commandService`
is `null`. |

##### Remarks

This constructor registers the command with Visual Studio's menu system
and attaches the
[OnBeforeQueryStatus](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-OnBeforeQueryStatus-System-Object,System-EventArgs- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.OnBeforeQueryStatus(System.Object,System.EventArgs)')
event handler to the command's
[](#E-Microsoft-VisualStudio-Shell-OleMenuCommand-BeforeQueryStatus 'Microsoft.VisualStudio.Shell.OleMenuCommand.BeforeQueryStatus')
event.



If `package` is `null`, an
[ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException')
is thrown.



If `commandService` is `null`, an
[ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') is thrown.

<a name='F-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-CommandId'></a>
### CommandId `constants`

##### Summary

The command identifier value that uniquely identifies this command
within the Visual Studio command system.

##### Remarks

This value must match the command ID defined in the `.vsct` file.

<a name='F-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-CommandSet'></a>
### CommandSet `constants`

##### Summary

The [Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') value that identifies the command set
to which this command belongs.

##### Remarks

This value must match the command set GUID defined in the `.vsct` file.

<a name='F-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-_commandService'></a>
### _commandService `constants`

##### Summary

Reference to an instance of an object that implements the
[IMenuCommandService](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ComponentModel.Design.IMenuCommandService 'System.ComponentModel.Design.IMenuCommandService') interface

<a name='F-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-_package'></a>
### _package `constants`

##### Summary

Reference to an instance of
[AsyncPackage](#T-Microsoft-VisualStudio-Shell-AsyncPackage 'Microsoft.VisualStudio.Shell.AsyncPackage')
that owns this
command.

##### Remarks

We must use the concrete
[AsyncPackage](#T-Microsoft-VisualStudio-Shell-AsyncPackage 'Microsoft.VisualStudio.Shell.AsyncPackage')
type here because
the Visual Studio extension infrastructure requires it for proper
package initialization and service retrieval.

<a name='P-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Instance'></a>
### Instance `property`

##### Summary

Gets a reference to an instance of
[CopyCoPilotReferencesCommand](#T-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand')
that represents the singleton instance of this command.

##### Remarks

This property is initialized by the
[InitializeAsync](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-InitializeAsync-Microsoft-VisualStudio-Shell-AsyncPackage- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.InitializeAsync(Microsoft.VisualStudio.Shell.AsyncPackage)')
method.



If accessed before initialization, this property will return
`null`
.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-#cctor'></a>
### #cctor() `method`

##### Summary

Initializes `static` data or performs actions that
need to be performed once only for the
[CopyCoPilotReferencesCommand](#T-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand')
class.

##### Parameters

This method has no parameters.

##### Remarks

This constructor is called automatically prior to the first instance
being created or before any `static` members are referenced.



We've decorated this constructor with the `[Log(AttributeExclude = true)]`
attribute in order to simplify the logging output.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-EndWaitDialog-Microsoft-VisualStudio-Shell-Interop-IVsThreadedWaitDialog2-'></a>
### EndWaitDialog(dialog) `method`

##### Summary

Ends the specified wait `dialog`, if it is active.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dialog | [Microsoft.VisualStudio.Shell.Interop.IVsThreadedWaitDialog2](#T-Microsoft-VisualStudio-Shell-Interop-IVsThreadedWaitDialog2 'Microsoft.VisualStudio.Shell.Interop.IVsThreadedWaitDialog2') | (Required.) Reference to an instance of an object that implements the
[IVsThreadedWaitDialog2](#T-Microsoft-VisualStudio-Shell-Interop-IVsThreadedWaitDialog2 'Microsoft.VisualStudio.Shell.Interop.IVsThreadedWaitDialog2')
interface.



This parameter can also be set to a `null` reference; in which
case, the method does nothing. |

##### Remarks

This method must be called on the UI thread.



If an exception occurs while ending the dialog, the exception is logged, and
the method continues execution without propagating the exception.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Execute-System-Object,System-EventArgs-'></a>
### Execute() `method`

##### Summary

Handles the command execution: obtains selected items, generates
Copilot reference(s) using the appropriate strategy, and copies them to
the clipboard.

##### Parameters

This method has no parameters.

##### Remarks

This method validates all input(s) and returns eagerly on invalid state.



All DTE access is kept on the UI thread (STA).



The method utilizes the
[TheReferenceGenerationStrategyToUse](#M-CCPRE-Generators-References-Actions-Determine-TheReferenceGenerationStrategyToUse-System-Object- 'CCPRE.Generators.References.Actions.Determine.TheReferenceGenerationStrategyToUse(System.Object)')
method to determine the appropriate
[CopilotReferenceGeneratorType](#T-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType')
for each selected item.



The method then obtains the corresponding
[ICopilotReferenceGenerator](#T-CCPRE-Generators-References-Interfaces-ICopilotReferenceGenerator 'CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator')
instance using the
[OfType](#M-CCPRE-Generators-References-Factories-GetCopilotReferenceGenerator-OfType-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType- 'CCPRE.Generators.References.Factories.GetCopilotReferenceGenerator.OfType(CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType)')
factory method.



If any generator cannot be obtained or reference generation fails, the
item is skipped and the method continues processing remaining item(s).

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-GenerateCopilotReferences-System-Collections-Generic-IReadOnlyList{EnvDTE-ProjectItem},System-String-'></a>
### GenerateCopilotReferences(projectItems,solutionDirectory) `method`

##### Summary

Generates GitHub Copilot reference(s) for the specified
[ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem') collection using the appropriate
strategy for each item.

##### Returns

Reference to a read-only list of [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') value(s)
containing the formatted GitHub Copilot reference(s), or an empty list if
no reference(s) could be generated.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| projectItems | [System.Collections.Generic.IReadOnlyList{EnvDTE.ProjectItem}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IReadOnlyList 'System.Collections.Generic.IReadOnlyList{EnvDTE.ProjectItem}') | (Required.) Reference to a read-only list of
[ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem') object(s) for which to generate
reference(s). |
| solutionDirectory | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that contains the absolute
path to the solution directory. |

##### Remarks

This method iterates through each [ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem') in
`projectItems` and determines the appropriate
[CopilotReferenceGeneratorType](#T-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType')
using the
[TheReferenceGenerationStrategyToUse](#M-CCPRE-Generators-References-Actions-Determine-TheReferenceGenerationStrategyToUse-System-Object- 'CCPRE.Generators.References.Actions.Determine.TheReferenceGenerationStrategyToUse(System.Object)')
method.



For each valid generator type, it obtains the corresponding
[ICopilotReferenceGenerator](#T-CCPRE-Generators-References-Interfaces-ICopilotReferenceGenerator 'CCPRE.Generators.References.Interfaces.ICopilotReferenceGenerator')
instance using the
[OfType](#M-CCPRE-Generators-References-Factories-GetCopilotReferenceGenerator-OfType-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType- 'CCPRE.Generators.References.Factories.GetCopilotReferenceGenerator.OfType(CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType)')
factory method.



This method returns an empty list if `projectItems` is
`null`.



This method returns an empty list if
`solutionDirectory` is blank or whitespace.



Item(s) for which no valid generator can be obtained are skipped and not
included in the result.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-GetSelectedProjectItems-EnvDTE80-DTE2-'></a>
### GetSelectedProjectItems(dte2) `method`

##### Summary

Gets the currently selected [ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem') entries from
Solution Explorer.
Falls back to the active document’s [ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem').

##### Returns

A read-only list of selected [ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem')
objects.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dte2 | [EnvDTE80.DTE2](#T-EnvDTE80-DTE2 'EnvDTE80.DTE2') | Reference to an instance of [DTE2](#T-EnvDTE80-DTE2 'EnvDTE80.DTE2'). |

##### Remarks

Eagerly returns an empty list when selection is unavailable. Does not walk the
UI hierarchy tree.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-GetSolutionDirectory-EnvDTE80-DTE2-'></a>
### GetSolutionDirectory(dte2) `method`

##### Summary

Computes the solution directory from [DTE2](#T-EnvDTE80-DTE2 'EnvDTE80.DTE2').

##### Returns

Absolute path to the solution directory, or `null`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dte2 | [EnvDTE80.DTE2](#T-EnvDTE80-DTE2 'EnvDTE80.DTE2') | Reference to an instance of [DTE2](#T-EnvDTE80-DTE2 'EnvDTE80.DTE2'). |

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-InitializeAsync-Microsoft-VisualStudio-Shell-AsyncPackage-'></a>
### InitializeAsync(package) `method`

##### Summary

Initializes the singleton instance of this command asynchronously.

##### Returns

A [Task](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.Tasks.Task 'System.Threading.Tasks.Task') that represents the
asynchronous initialization operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| package | [Microsoft.VisualStudio.Shell.AsyncPackage](#T-Microsoft-VisualStudio-Shell-AsyncPackage 'Microsoft.VisualStudio.Shell.AsyncPackage') | (Required.) Reference to an instance of
[AsyncPackage](#T-Microsoft-VisualStudio-Shell-AsyncPackage 'Microsoft.VisualStudio.Shell.AsyncPackage')
that owns this
command. |

##### Remarks

This method switches to the main UI thread, retrieves the menu command
service, and constructs the singleton instance of
[CopyCoPilotReferencesCommand](#T-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand')
.



If `package` is `null`, the method
will fail and the
[Instance](#P-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Instance 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.Instance')
property will remain `null`.



If the menu command service cannot be retrieved, initialization may fail
and the
[Instance](#P-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Instance 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.Instance')
property will remain `null`.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-JoinReferences-System-Collections-Generic-IReadOnlyList{System-String}-'></a>
### JoinReferences(tokens) `method`

##### Summary

Joins tokens into a single space-separated string.

##### Returns

A single string suitable for the clipboard.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tokens | [System.Collections.Generic.IReadOnlyList{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IReadOnlyList 'System.Collections.Generic.IReadOnlyList{System.String}') | Reference to a list of tokens. |

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-NormalizeSeparators-System-String-'></a>
### NormalizeSeparators() `method`

##### Summary

Normalizes path separators to forward slashes for Copilot Chat.

##### Parameters

This method has no parameters.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-OnBeforeQueryStatus-System-Object,System-EventArgs-'></a>
### OnBeforeQueryStatus(sender,e) `method`

##### Summary

Handles the
[](#E-Microsoft-VisualStudio-Shell-OleMenuCommand-BeforeQueryStatus 'Microsoft.VisualStudio.Shell.OleMenuCommand.BeforeQueryStatus')
event to control the visibility and enabled state of the command.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sender | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | (Required.) Reference to an instance of [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object')
that represents the source of the event, expected to be an instance of
[OleMenuCommand](#T-Microsoft-VisualStudio-Shell-OleMenuCommand 'Microsoft.VisualStudio.Shell.OleMenuCommand'). |
| e | [System.EventArgs](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.EventArgs 'System.EventArgs') | (Required.) Reference to an instance of [EventArgs](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.EventArgs 'System.EventArgs') that
contains the event data. |

##### Remarks

This method ensures the command is visible and enabled whenever it is
queried.



If `sender` is not an instance of
[OleMenuCommand](#T-Microsoft-VisualStudio-Shell-OleMenuCommand 'Microsoft.VisualStudio.Shell.OleMenuCommand')
, no action is
taken.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-StartWaitDialog-System-String,System-String,System-String-'></a>
### StartWaitDialog(caption,message,status) `method`

##### Summary

Creates and starts a `Threaded Wait Dialog` with the specified
`caption`, `message`, and
`status` text.

##### Returns

Reference to an instance of an object that implements the
[IVsThreadedWaitDialog2](#T-Microsoft-VisualStudio-Shell-Interop-IVsThreadedWaitDialog2 'Microsoft.VisualStudio.Shell.Interop.IVsThreadedWaitDialog2')
interface representing the active `Wait Dialog`, or
`null` if the dialog could not be created.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| caption | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') containing
the caption text to display in the titlebar of the `Wait Dialog`. |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') containing
the main message text to display in the `Wait Dialog`. |
| status | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') containing
the status text to display in the `Wait Dialog`, providing additional
context or progress information. |

##### Remarks

This method initializes and displays a `Threaded Wait Dialog`
using Visual Studio's `Wait Dialog Service` component..



If the service is unavailable or an error occurs during initialization, the
method returns `null`.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-TryCopyToClipboard-System-String-'></a>
### TryCopyToClipboard(text) `method`

##### Summary

Makes an attempt to copy the specified `text` to the
Clipboard.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') containing the text that is to be
copied to the Clipboard. |

##### Remarks

If the specified `text` is `null`,
blank, or the [Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty') value, then the method does
nothing.

<a name='T-CopyCoPilotReferencesExtension-CopyCoPilotReferencesPackage'></a>
## CopyCoPilotReferencesPackage `type`

##### Namespace

CopyCoPilotReferencesExtension

##### Summary

Represents the Visual Studio package for the `CopyCoPilotReferences`
extension.

##### Remarks

This package is registered with Visual Studio to provide the functionality of
the
`CopyCoPilotReferences` extension.



It supports asynchronous initialization and background loading to improve
performance during Visual Studio startup.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesPackage-#ctor'></a>
### #ctor() `constructor`

##### Summary

Creates a new instance of
[CopyCoPilotReferencesPackage](#T-CopyCoPilotReferencesExtension-CopyCoPilotReferencesPackage 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesPackage')
and returns a reference to it.

##### Parameters

This constructor has no parameters.

##### Remarks

We've decorated this constructor with the
`[Log(AttributeExclude = true)]` attribute in order to simplify the
logging output.

<a name='F-CopyCoPilotReferencesExtension-CopyCoPilotReferencesPackage-PackageGuidString'></a>
### PackageGuidString `constants`

##### Summary

The [Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') string for the package.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesPackage-#cctor'></a>
### #cctor() `method`

##### Summary

Initializes `static` data or performs actions that
need to be performed once only for the
[CopyCoPilotReferencesPackage](#T-CopyCoPilotReferencesExtension-CopyCoPilotReferencesPackage 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesPackage')
class.

##### Parameters

This method has no parameters.

##### Remarks

This constructor is called automatically prior to the first instance
being created or before any `static` members are referenced.



We've decorated this constructor with the `[Log(AttributeExclude = true)]`
attribute in order to simplify the logging output.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesPackage-InitializeAsync-System-Threading-CancellationToken,System-IProgress{Microsoft-VisualStudio-Shell-ServiceProgressData}-'></a>
### InitializeAsync(cancellationToken,progress) `method`

##### Summary

The async initialization portion of the package initialization process. This
method is invoked from a background thread.

##### Returns

A task representing the async work of package initialization, or an
already completed task if there is none. Do not return null from this method.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | A cancellation token to monitor for
initialization cancellation, which can occur when VS is shutting down. |
| progress | [System.IProgress{Microsoft.VisualStudio.Shell.ServiceProgressData}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IProgress 'System.IProgress{Microsoft.VisualStudio.Shell.ServiceProgressData}') | (Required.) Reference to an instance of an object that implements the
[IProgress\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IProgress`1 'System.IProgress`1') interface that iṡ provided with a reference
to [ServiceProgressData](#T-Microsoft-VisualStudio-Shell-ServiceProgressData 'Microsoft.VisualStudio.Shell.ServiceProgressData') as its
type parameter.



This object is used to report progress during initialization. |

<a name='T-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Get'></a>
## Get `type`

##### Namespace

CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand

##### Summary

Exposes static methods to obtain data from various data sources.

<a name='F-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Get-LOG_FILE_PATH_TERMINATOR'></a>
### LOG_FILE_PATH_TERMINATOR `constants`

##### Summary

A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') containing the final piece of the path of the
log file.

<a name='P-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Get-AssemblyCompany'></a>
### AssemblyCompany `property`

##### Summary

Gets a [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that contains the product name defined
for this application.

##### Remarks

This property is really an alias for the
[AssemblyCompany](#P-xyLOGIX-Core-Assemblies-Info-AssemblyMetadata-AssemblyCompany 'xyLOGIX.Core.Assemblies.Info.AssemblyMetadata.AssemblyCompany')
property.

<a name='P-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Get-AssemblyProduct'></a>
### AssemblyProduct `property`

##### Summary

Gets a [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that contains the product name defined
for this application.

##### Remarks

This property is really an alias for the
[ShortProductName](#P-xyLOGIX-Core-Assemblies-Info-AssemblyMetadata-ShortProductName 'xyLOGIX.Core.Assemblies.Info.AssemblyMetadata.ShortProductName')
property.

<a name='P-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Get-AssemblyTitle'></a>
### AssemblyTitle `property`

##### Summary

Gets a [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that contains the assembly title defined
for this application.

##### Remarks

This property is really an alias for the
[AssemblyTitle](#P-xyLOGIX-Core-Assemblies-Info-AssemblyMetadata-AssemblyTitle 'xyLOGIX.Core.Assemblies.Info.AssemblyMetadata.AssemblyTitle')
property --- except that all whitespace is replace with underscores.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Get-ApplicationProductName'></a>
### ApplicationProductName() `method`

##### Summary

Gets a [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that contains a user-friendly name for
the software product of which this application or class library is a part.

##### Returns

A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that contains a user-friendly name for the
software product of which this application or class library is a part.

##### Parameters

This method has no parameters.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Get-LogFilePath'></a>
### LogFilePath() `method`

##### Summary

Obtains a [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that contains the fully-qualified
pathname of the file that should be used for logging messages.

##### Returns

A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that contains the fully-qualified pathname of
the file that should be used for logging messages.

##### Parameters

This method has no parameters.
