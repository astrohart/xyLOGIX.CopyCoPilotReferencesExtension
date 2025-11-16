<a name='assembly'></a>
# CCPRE.Generators.References.Actions

## Contents

- [AskWhether](#T-CCPRE-Generators-References-Actions-AskWhether 'CCPRE.Generators.References.Actions.AskWhether')
  - [#cctor()](#M-CCPRE-Generators-References-Actions-AskWhether-#cctor 'CCPRE.Generators.References.Actions.AskWhether.#cctor')
  - [ProjectItemRepresentsPhysicalFile(projectItem)](#M-CCPRE-Generators-References-Actions-AskWhether-ProjectItemRepresentsPhysicalFile-EnvDTE-ProjectItem- 'CCPRE.Generators.References.Actions.AskWhether.ProjectItemRepresentsPhysicalFile(EnvDTE.ProjectItem)')
- [Determine](#T-CCPRE-Generators-References-Actions-Determine 'CCPRE.Generators.References.Actions.Determine')
  - [#cctor()](#M-CCPRE-Generators-References-Actions-Determine-#cctor 'CCPRE.Generators.References.Actions.Determine.#cctor')
  - [TheReferenceGenerationStrategyToUse(selectedObject)](#M-CCPRE-Generators-References-Actions-Determine-TheReferenceGenerationStrategyToUse-System-Object- 'CCPRE.Generators.References.Actions.Determine.TheReferenceGenerationStrategyToUse(System.Object)')
- [Resources](#T-CCPRE-Generators-References-Actions-Properties-Resources 'CCPRE.Generators.References.Actions.Properties.Resources')
  - [Culture](#P-CCPRE-Generators-References-Actions-Properties-Resources-Culture 'CCPRE.Generators.References.Actions.Properties.Resources.Culture')
  - [ResourceManager](#P-CCPRE-Generators-References-Actions-Properties-Resources-ResourceManager 'CCPRE.Generators.References.Actions.Properties.Resources.ResourceManager')

<a name='T-CCPRE-Generators-References-Actions-AskWhether'></a>
## AskWhether `type`

##### Namespace

CCPRE.Generators.References.Actions

##### Summary

Provides utility method(s) for evaluating and determining specific
characteristics of [ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem')(s).

##### Remarks

The [AskWhether](#T-CCPRE-Generators-References-Actions-AskWhether 'CCPRE.Generators.References.Actions.AskWhether')
class contains static methods designed to assist in analyzing
[ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem')(s), such as determining whether a given
[ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem')represents a physical file.



This class is not intended to be instantiated.

<a name='M-CCPRE-Generators-References-Actions-AskWhether-#cctor'></a>
### #cctor() `method`

##### Summary

Initializes `static` data or performs actions that
need to be performed once only for the
[AskWhether](#T-CCPRE-Generators-References-Actions-AskWhether 'CCPRE.Generators.References.Actions.AskWhether') class.

##### Parameters

This method has no parameters.

##### Remarks

This constructor is called automatically prior to the first instance
being created or before any `static` members are referenced.



We've decorated this constructor with the `[Log(AttributeExclude = true)]`
attribute in order to simplify the logging output.

<a name='M-CCPRE-Generators-References-Actions-AskWhether-ProjectItemRepresentsPhysicalFile-EnvDTE-ProjectItem-'></a>
### ProjectItemRepresentsPhysicalFile(projectItem) `method`

##### Summary

Determines whether the specified `projectItem` represents a
physical file in the corresponding `Project`.

##### Returns

`true` if the `projectItem`
represents a physical file; otherwise, `false`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| projectItem | [EnvDTE.ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem') | (Required.) Reference to an instance of an object that implements the
[ProjectItem](#T-EnvDTE-ProjectItem 'EnvDTE.ProjectItem') interface that is to be evaluated.



A `null` reference may not be passed for the argument of this
parameter. |

##### Remarks

This method checks the value of the
[Kind](#P-EnvDTE-ProjectItem-Kind 'EnvDTE.ProjectItem.Kind') property of the specified
`projectItem` to determine if it matches the constant value
[vsProjectItemKindPhysicalFile](#F-EnvDTE-Constants-vsProjectItemKindPhysicalFile 'EnvDTE.Constants.vsProjectItemKindPhysicalFile').



If the argument of the `projectItem` parameter is set to a
`null` reference, or if the value of the
[Kind](#P-EnvDTE-ProjectItem-Kind 'EnvDTE.ProjectItem.Kind') property is set to a
`null`[String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String'), a blank
[String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String'), or the [Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty')
value, then the method returns `false`.

<a name='T-CCPRE-Generators-References-Actions-Determine'></a>
## Determine `type`

##### Namespace

CCPRE.Generators.References.Actions

##### Summary

Provides method(s) for determining which
[CopilotReferenceGeneratorType](#T-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType')
strategy to use based on Visual Studio selection(s).

<a name='M-CCPRE-Generators-References-Actions-Determine-#cctor'></a>
### #cctor() `method`

##### Summary

Initializes `static` data or performs actions that
need to be performed once only for the
[Determine](#T-CCPRE-Generators-References-Actions-Determine 'CCPRE.Generators.References.Actions.Determine')
class.

##### Parameters

This method has no parameters.

##### Remarks

This constructor is called automatically prior to the first instance
being created or before any `static` members are referenced.



We've decorated this constructor with the `[Log(AttributeExclude = true)]`
attribute in order to simplify the logging output.

<a name='M-CCPRE-Generators-References-Actions-Determine-TheReferenceGenerationStrategyToUse-System-Object-'></a>
### TheReferenceGenerationStrategyToUse(selectedObject) `method`

##### Summary

Determines the appropriate
[CopilotReferenceGeneratorType](#T-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType')
to use based on the specified Visual Studio object.

##### Returns

A
[CopilotReferenceGeneratorType](#T-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType')
value that indicates which strategy should be used to generate the
Copilot reference, or
[Unknown](#F-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType-Unknown 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType.Unknown')
if the type cannot be determined.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| selectedObject | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | (Required.) Reference to an instance of [Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object')
that represents the selected Visual Studio element. |

##### Remarks

This method inspects the type and properties of
`selectedObject` to determine which generator strategy
is appropriate.



This method returns
[Unknown](#F-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType-Unknown 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType.Unknown')
if `selectedObject` is `null`.



This method returns
[Unknown](#F-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType-Unknown 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType.Unknown')
if `selectedObject` is of an unrecognized type.

<a name='T-CCPRE-Generators-References-Actions-Properties-Resources'></a>
## Resources `type`

##### Namespace

CCPRE.Generators.References.Actions.Properties

##### Summary

A strongly-typed resource class, for looking up localized strings, etc.

<a name='P-CCPRE-Generators-References-Actions-Properties-Resources-Culture'></a>
### Culture `property`

##### Summary

Overrides the current thread's CurrentUICulture property for all
  resource lookups using this strongly typed resource class.

<a name='P-CCPRE-Generators-References-Actions-Properties-Resources-ResourceManager'></a>
### ResourceManager `property`

##### Summary

Returns the cached ResourceManager instance used by this class.
