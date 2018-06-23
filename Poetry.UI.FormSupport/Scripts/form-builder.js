


/* FORM BUILDER */

class FormBuilder {
    build(target, formFields, formFieldTypes, translations) {
        var root = document.createElement('div');

        formFields.forEach(formField => {
            if (!formField.AutoGenerate) {
                return;
            }

            var formFieldType = formFieldTypes[formField.Type];

            if (!formFieldType) {
                throw new Error(`Form field type ${formField.Type} not found`);
            }

            formField.Name = translations[formField.Id] || formField.Label || formField.Id;

            root.appendChild(formFieldType.createControl(formField, () => target[formField.Id], value => target[formField.Id] = value));
        });

        return root;
    }
}

export default FormBuilder;