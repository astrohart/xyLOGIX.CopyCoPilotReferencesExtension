<a name='assembly'></a>
# CCPRE.Generators.References.Validators.Interfaces

## Contents

- [ICopilotReferenceGeneratorTypeValidator](#T-CCPRE-Generators-References-Validators-Interfaces-ICopilotReferenceGeneratorTypeValidator 'CCPRE.Generators.References.Validators.Interfaces.ICopilotReferenceGeneratorTypeValidator')
  - [IsValid(type)](#M-CCPRE-Generators-References-Validators-Interfaces-ICopilotReferenceGeneratorTypeValidator-IsValid-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType- 'CCPRE.Generators.References.Validators.Interfaces.ICopilotReferenceGeneratorTypeValidator.IsValid(CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType)')
- [Resources](#T-CCPRE-Generators-References-Validators-Interfaces-Properties-Resources 'CCPRE.Generators.References.Validators.Interfaces.Properties.Resources')
  - [Culture](#P-CCPRE-Generators-References-Validators-Interfaces-Properties-Resources-Culture 'CCPRE.Generators.References.Validators.Interfaces.Properties.Resources.Culture')
  - [ResourceManager](#P-CCPRE-Generators-References-Validators-Interfaces-Properties-Resources-ResourceManager 'CCPRE.Generators.References.Validators.Interfaces.Properties.Resources.ResourceManager')

<a name='T-CCPRE-Generators-References-Validators-Interfaces-ICopilotReferenceGeneratorTypeValidator'></a>
## ICopilotReferenceGeneratorTypeValidator `type`

##### Namespace

CCPRE.Generators.References.Validators.Interfaces

##### Summary

Defines the publicly-exposed events, methods and properties of a validator of
[CopilotReferenceGeneratorType](#T-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType')
enumeration values.

##### Remarks

Specifically, objects that implement this interface ascertain whether the
values of variables fall within the value set that is defined by the
[CopilotReferenceGeneratorType](#T-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType')
enumeration.

<a name='M-CCPRE-Generators-References-Validators-Interfaces-ICopilotReferenceGeneratorTypeValidator-IsValid-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType-'></a>
### IsValid(type) `method`

##### Summary

Determines whether the copilot reference generator `type`
value passed is within the value set that is defined by the
[CopilotReferenceGeneratorType](#T-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType')
enumeration.

##### Returns

`true` if the copilot reference generator
`type` falls within the defined value set;
`false` otherwise.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType](#T-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType') | (Required.) One of the
[CopilotReferenceGeneratorType](#T-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType')
values that is to be examined. |

<a name='T-CCPRE-Generators-References-Validators-Interfaces-Properties-Resources'></a>
## Resources `type`

##### Namespace

CCPRE.Generators.References.Validators.Interfaces.Properties

##### Summary

A strongly-typed resource class, for looking up localized strings, etc.

<a name='P-CCPRE-Generators-References-Validators-Interfaces-Properties-Resources-Culture'></a>
### Culture `property`

##### Summary

Overrides the current thread's CurrentUICulture property for all
  resource lookups using this strongly typed resource class.

<a name='P-CCPRE-Generators-References-Validators-Interfaces-Properties-Resources-ResourceManager'></a>
### ResourceManager `property`

##### Summary

Returns the cached ResourceManager instance used by this class.
