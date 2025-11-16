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
  - [AppendDirectorySeparator()](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-AppendDirectorySeparator-System-String- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.AppendDirectorySeparator(System.String)')
  - [CollectAbsolutePaths(projectItems)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-CollectAbsolutePaths-System-Collections-Generic-IReadOnlyList{EnvDTE-ProjectItem}- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.CollectAbsolutePaths(System.Collections.Generic.IReadOnlyList{EnvDTE.ProjectItem})')
  - [EndWaitDialog()](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-EndWaitDialog-Microsoft-VisualStudio-Shell-Interop-IVsThreadedWaitDialog2- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.EndWaitDialog(Microsoft.VisualStudio.Shell.Interop.IVsThreadedWaitDialog2)')
  - [Execute()](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Execute-System-Object,System-EventArgs- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.Execute(System.Object,System.EventArgs)')
  - [FormatCopilotReference()](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-FormatCopilotReference-System-String- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.FormatCopilotReference(System.String)')
  - [GetSelectedProjectItems(dte2)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-GetSelectedProjectItems-EnvDTE80-DTE2- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.GetSelectedProjectItems(EnvDTE80.DTE2)')
  - [GetSolutionDirectory(dte2)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-GetSolutionDirectory-EnvDTE80-DTE2- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.GetSolutionDirectory(EnvDTE80.DTE2)')
  - [InitializeAsync(package)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-InitializeAsync-Microsoft-VisualStudio-Shell-AsyncPackage- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.InitializeAsync(Microsoft.VisualStudio.Shell.AsyncPackage)')
  - [JoinReferences(tokens)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-JoinReferences-System-Collections-Generic-IReadOnlyList{System-String}- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.JoinReferences(System.Collections.Generic.IReadOnlyList{System.String})')
  - [NormalizeSeparators()](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-NormalizeSeparators-System-String- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.NormalizeSeparators(System.String)')
  - [OnBeforeQueryStatus(sender,e)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-OnBeforeQueryStatus-System-Object,System-EventArgs- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.OnBeforeQueryStatus(System.Object,System.EventArgs)')
  - [ProcessProjectItem(projectItem)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-ProcessProjectItem-EnvDTE-ProjectItem- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.ProcessProjectItem(EnvDTE.ProjectItem)')
  - [StartWaitDialog()](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-StartWaitDialog-System-String,System-String,System-String- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.StartWaitDialog(System.String,System.String,System.String)')
  - [ToSolutionRelative(absolutePath,solutionDir)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-ToSolutionRelative-System-String,System-String- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.ToSolutionRelative(System.String,System.String)')
  - [TransformToCopilotReferences(absolutePaths,solutionDir)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-TransformToCopilotReferences-System-Collections-Generic-IReadOnlyList{System-String},System-String- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.TransformToCopilotReferences(System.Collections.Generic.IReadOnlyList{System.String},System.String)')
  - [TryCopyToClipboard()](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-TryCopyToClipboard-System-String- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.TryCopyToClipboard(System.String)')
- [CopyCoPilotReferencesPackage](#T-CopyCoPilotReferencesExtension-CopyCoPilotReferencesPackage 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesPackage')
  - [#ctor()](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesPackage-#ctor 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesPackage.#ctor')
  - [PackageGuidString](#F-CopyCoPilotReferencesExtension-CopyCoPilotReferencesPackage-PackageGuidString 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesPackage.PackageGuidString')
  - [#cctor()](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesPackage-#cctor 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesPackage.#cctor')
  - [InitializeAsync(cancellationToken,progress)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesPackage-InitializeAsync-System-Threading-CancellationToken,System-IProgress{Microsoft-VisualStudio-Shell-ServiceProgressData}- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesPackage.InitializeAsync(System.Threading.CancellationToken,System.IProgress{Microsoft.VisualStudio.Shell.ServiceProgressData})')

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

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-AppendDirectorySeparator-System-String-'></a>
### AppendDirectorySeparator() `method`

##### Summary

Ensures trailing directory separator (for
[MakeRelativeUri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri.MakeRelativeUri 'System.Uri.MakeRelativeUri(System.Uri)') correctness).

##### Parameters

This method has no parameters.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-CollectAbsolutePaths-System-Collections-Generic-IReadOnlyList{EnvDTE-ProjectItem}-'></a>
### CollectAbsolutePaths(projectItems) `method`

##### Summary

Converts [ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem') entries into absolute file system
paths.

##### Returns

A list of absolute file paths.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| projectItems | [System.Collections.Generic.IReadOnlyList{EnvDTE.ProjectItem}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IReadOnlyList 'System.Collections.Generic.IReadOnlyList{EnvDTE.ProjectItem}') | Reference to a read-only list of
[ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem'). |

##### Remarks

Uses only FileNames[1] for each item; skips non-physical items, folders, or
items without files.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-EndWaitDialog-Microsoft-VisualStudio-Shell-Interop-IVsThreadedWaitDialog2-'></a>
### EndWaitDialog() `method`

##### Summary

Ends the wait dialog if it was shown.

##### Parameters

This method has no parameters.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Execute-System-Object,System-EventArgs-'></a>
### Execute() `method`

##### Summary

Handles the command execution: obtains selected items, extracts full paths,
converts to solution-relative #file:'…' refs, and copies to the clipboard.

##### Parameters

This method has no parameters.

##### Remarks

Validates all inputs and returns eagerly on invalid state. All DTE access
is kept on the UI thread (STA). Only pure path/formatting work is parallelized.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-FormatCopilotReference-System-String-'></a>
### FormatCopilotReference() `method`

##### Summary

Formats a path as a Copilot Chat file reference token.

##### Parameters

This method has no parameters.

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

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-ProcessProjectItem-EnvDTE-ProjectItem-'></a>
### ProcessProjectItem(projectItem) `method`

##### Summary

Extracts the absolute path for a single [ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem') if
it is a physical file.

##### Returns

The absolute file path, or `null` on failure.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| projectItem | [EnvDTE.ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem') | Reference to a [ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem'). |

##### Remarks

Skips non-physical items and items with no files. Bounds-checks FileCount and
FileNames[1].

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-StartWaitDialog-System-String,System-String,System-String-'></a>
### StartWaitDialog() `method`

##### Summary

Shows the VS Threaded Wait Dialog with a marquee progress bar.

##### Parameters

This method has no parameters.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-ToSolutionRelative-System-String,System-String-'></a>
### ToSolutionRelative(absolutePath,solutionDir) `method`

##### Summary

Converts an absolute path to a solution-relative path.

##### Returns

A relative path using forward slashes, or `null`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| absolutePath | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Reference to an absolute file path. |
| solutionDir | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Reference to the absolute solution directory. |

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-TransformToCopilotReferences-System-Collections-Generic-IReadOnlyList{System-String},System-String-'></a>
### TransformToCopilotReferences(absolutePaths,solutionDir) `method`

##### Summary

Transforms absolute paths into solution-relative Copilot Chat references in
parallel.

##### Returns

A read-only list of formatted `#file:'…'` tokens.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| absolutePaths | [System.Collections.Generic.IReadOnlyList{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IReadOnlyList 'System.Collections.Generic.IReadOnlyList{System.String}') | Reference to a list of absolute paths. |
| solutionDir | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Reference to the absolute solution directory. |

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-TryCopyToClipboard-System-String-'></a>
### TryCopyToClipboard() `method`

##### Summary

Copies text to the clipboard on the UI thread.

##### Parameters

This method has no parameters.

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
