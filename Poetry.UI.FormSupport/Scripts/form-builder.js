


/* FORM BUILDER */

class FormBuilder {
    build(target, formFields, formFieldTypes, translations) {
        var root = document.createElement('div');

        formFields.forEach(formField => {
            var formFieldType = formFieldTypes[formField.Type];

            formField.Name = translations[formField.Id] || formField.Label || formField.Id;

            root.appendChild(formFieldType.createControl(formField, () => target[formField.Id], value => target[formField.Id] = value));
        });

        return root;
    }
}