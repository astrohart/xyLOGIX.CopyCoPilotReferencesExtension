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
  - [ServiceProvider](#P-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-ServiceProvider 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.ServiceProvider')
  - [Execute(sender,e)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Execute-System-Object,System-EventArgs- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.Execute(System.Object,System.EventArgs)')
  - [GetCopilotReference(projectItem,solutionDir)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-GetCopilotReference-EnvDTE-ProjectItem,System-String- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.GetCopilotReference(EnvDTE.ProjectItem,System.String)')
  - [GetRelativePath(basePath,targetPath)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-GetRelativePath-System-String,System-String- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.GetRelativePath(System.String,System.String)')
  - [InitializeAsync(package)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-InitializeAsync-Microsoft-VisualStudio-Shell-AsyncPackage- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.InitializeAsync(Microsoft.VisualStudio.Shell.AsyncPackage)')
  - [OnBeforeQueryStatus(sender,e)](#M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-OnBeforeQueryStatus-System-Object,System-EventArgs- 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.OnBeforeQueryStatus(System.Object,System.EventArgs)')

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

The purpose of this field is to cache the value of the
[Package](#P-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Package 'CopyCoPilotReferencesExtension.CopyCoPilotReferencesCommand.Package')
property.



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

<a name='P-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-ServiceProvider'></a>
### ServiceProvider `property`

##### Summary

Gets a reference to an instance of
[IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider')
that provides access to Visual
Studio service(s).

##### Remarks

This property returns the underlying
[AsyncPackage](#T-Microsoft-VisualStudio-Shell-AsyncPackage 'Microsoft.VisualStudio.Shell.AsyncPackage')
as an
[IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider')
to follow interface-based design
principles.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-Execute-System-Object,System-EventArgs-'></a>
### Execute(sender,e) `method`

##### Summary

Executes the command logic to copy selected file reference(s) to the
clipboard.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sender | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | (Required.) Reference to an instance of [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object')
that represents the source of the event. |
| e | [System.EventArgs](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.EventArgs 'System.EventArgs') | (Required.) Reference to an instance of [EventArgs](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.EventArgs 'System.EventArgs') that
contains the event data. |

##### Remarks

This method retrieves selected file(s) from Solution Explorer, computes
their path(s) relative to the solution directory, formats them as
`#file:'relativePath'` reference(s), and copies the formatted
reference(s) to the clipboard.



The relative path(s) use forward slash(es) (`/`) as separator(s)
for compatibility with GitHub Copilot's reference format.



If no valid file(s) are selected, the method returns early without
modifying the clipboard.



If the DTE service is unavailable, the method returns early without
modifying the clipboard.



If the solution is not loaded, the method returns early without
modifying the clipboard.



This method must be called on the UI thread; otherwise, an exception
will be thrown by
[ThrowIfNotOnUIThread](#M-Microsoft-VisualStudio-Shell-ThreadHelper-ThrowIfNotOnUIThread-System-String- 'Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread(System.String)')
.

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-GetCopilotReference-EnvDTE-ProjectItem,System-String-'></a>
### GetCopilotReference(projectItem,solutionDir) `method`

##### Summary

Gets a GitHub Copilot reference string for the specified
`projectItem`
.

##### Returns

A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') containing the GitHub Copilot
reference in the format `#file:'relativePath'`, or
[Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty')
if the reference cannot be generated.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| projectItem | [EnvDTE.ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem') | (Required.) Reference to an instance of
[ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem')
for which to generate the reference. |
| solutionDir | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that contains the solution
directory path. |

##### Remarks

This method computes the relative path from the solution directory to
the project item's file and formats it as a GitHub Copilot reference.



If `projectItem` is `null`, the
method returns [Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty').



If `solutionDir` is blank or whitespace, the method
returns [Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty').

<a name='M-CopyCoPilotReferencesExtension-CopyCoPilotReferencesCommand-GetRelativePath-System-String,System-String-'></a>
### GetRelativePath(basePath,targetPath) `method`

##### Summary

Computes a relative path from `basePath` to
`targetPath`
.

##### Returns

A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') containing the relative path from
`basePath` to `targetPath`, or
`targetPath` if a relative path cannot be computed.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| basePath | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that contains the base
directory path from which to compute the relative path. |
| targetPath | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that contains the target
file or directory path for which to compute the relative path. |

##### Remarks

This method provides a lightweight implementation of
`Path.GetRelativePath` for .NET Framework 4.8 compatibility, using
[Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri')-based logic.



If `basePath` is blank or whitespace, the method
returns `targetPath` (or
[Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty')
if `targetPath` is
also blank).



If `targetPath` is blank or whitespace, the method
returns [Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty').



If the path(s) are on different volume(s) or scheme(s), the method
returns `targetPath` unchanged.



If URI construction fails, the method returns
`targetPath`
unchanged.

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
