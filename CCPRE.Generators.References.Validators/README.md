<a name='assembly'></a>
# CCPRE.Generators.References.Validators

## Contents

- [CopilotReferenceGeneratorTypeValidator](#T-CCPRE-Generators-References-Validators-CopilotReferenceGeneratorTypeValidator 'CCPRE.Generators.References.Validators.CopilotReferenceGeneratorTypeValidator')
  - [#ctor()](#M-CCPRE-Generators-References-Validators-CopilotReferenceGeneratorTypeValidator-#ctor 'CCPRE.Generators.References.Validators.CopilotReferenceGeneratorTypeValidator.#ctor')
  - [Instance](#P-CCPRE-Generators-References-Validators-CopilotReferenceGeneratorTypeValidator-Instance 'CCPRE.Generators.References.Validators.CopilotReferenceGeneratorTypeValidator.Instance')
  - [#cctor()](#M-CCPRE-Generators-References-Validators-CopilotReferenceGeneratorTypeValidator-#cctor 'CCPRE.Generators.References.Validators.CopilotReferenceGeneratorTypeValidator.#cctor')
  - [IsValid(type)](#M-CCPRE-Generators-References-Validators-CopilotReferenceGeneratorTypeValidator-IsValid-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType- 'CCPRE.Generators.References.Validators.CopilotReferenceGeneratorTypeValidator.IsValid(CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType)')
- [Resources](#T-CCPRE-Generators-References-Validators-Properties-Resources 'CCPRE.Generators.References.Validators.Properties.Resources')
  - [Culture](#P-CCPRE-Generators-References-Validators-Properties-Resources-Culture 'CCPRE.Generators.References.Validators.Properties.Resources.Culture')
  - [ResourceManager](#P-CCPRE-Generators-References-Validators-Properties-Resources-ResourceManager 'CCPRE.Generators.References.Validators.Properties.Resources.ResourceManager')

<a name='T-CCPRE-Generators-References-Validators-CopilotReferenceGeneratorTypeValidator'></a>
## CopilotReferenceGeneratorTypeValidator `type`

##### Namespace

CCPRE.Generators.References.Validators

##### Summary

Validates whether certain value(s) are within the defined value set of the
[CopilotReferenceGeneratorType](#T-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType 'CCPRE.Generators.References.Constants.CopilotReferenceGeneratorType')
enumeration.

<a name='M-CCPRE-Generators-References-Validators-CopilotReferenceGeneratorTypeValidator-#ctor'></a>
### #ctor() `constructor`

##### Summary

Empty, `private` constructor to prohibit direct
allocation of this class.

##### Parameters

This constructor has no parameters.

##### Remarks

Even though this constructor be marked `private`, we
can still perform initialization of this object here.

<a name='P-CCPRE-Generators-References-Validators-CopilotReferenceGeneratorTypeValidator-Instance'></a>
### Instance `property`

##### Summary

Gets a reference to the one and only instance of the object that implements the
[ICopilotReferenceGeneratorTypeValidator](#T-CCPRE-Generators-References-Validators-Interfaces-ICopilotReferenceGeneratorTypeValidator 'CCPRE.Generators.References.Validators.Interfaces.ICopilotReferenceGeneratorTypeValidator')
interface.

<a name='M-CCPRE-Generators-References-Validators-CopilotReferenceGeneratorTypeValidator-#cctor'></a>
### #cctor() `method`

##### Summary

Initializes `static` data or performs actions that need to be
performed once only for the
[CopilotReferenceGeneratorTypeValidator](#T-CCPRE-Generators-References-Validators-CopilotReferenceGeneratorTypeValidator 'CCPRE.Generators.References.Validators.CopilotReferenceGeneratorTypeValidator')
class.

##### Parameters

This method has no parameters.

##### Remarks

This constructor is called automatically prior to the first instance
being created or before any `static` members are referenced.



We've decorated this constructor with the `[Log(AttributeExclude = true)]`
attribute in order to simplify the logging output.



This particular constructor is here to prohibit direct instantiation of this
`Singleton` object.

<a name='M-CCPRE-Generators-References-Validators-CopilotReferenceGeneratorTypeValidator-IsValid-CCPRE-Generators-References-Constants-CopilotReferenceGeneratorType-'></a>
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

<a name='T-CCPRE-Generators-References-Validators-Properties-Resources'></a>
## Resources `type`

##### Namespace

CCPRE.Generators.References.Validators.Properties

##### Summary

A strongly-typed resource class, for looking up localized strings, etc.

<a name='P-CCPRE-Generators-References-Validators-Properties-Resources-Culture'></a>
### Culture `property`

##### Summary

Overrides the current thread's CurrentUICulture property for all
  resource lookups using this strongly typed resource class.

<a name='P-CCPRE-Generators-References-Validators-Properties-Resources-ResourceManager'></a>
### ResourceManager `property`

##### Summary

Returns the cached ResourceManager instance used by this class.
