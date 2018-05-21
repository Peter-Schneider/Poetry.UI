


/* FORM BUILDER */

class FormBuilder {
    build(target, formFields, formFieldTypes, translations) {
        var container = document.createElement('div');

        formFields.forEach(formField => {
            var formFieldType = formFieldTypes[formField.Type];

            formField.Name = translations[formField.Id] || formField.Label || formField.Id;

            container.appendChild(formFieldType.createControl(formField, () => target[formField.Id], value => target[formField.Id] = value));
        });

        return container;
    }
}

var injections = injections || [];

injections.push(['formBuilder', new FormBuilder()]);