


/* FORM BUILDER */

class FormBuilder {
    build(target, propertyDefinitions, formElements) {
        var container = document.createElement('div');

        propertyDefinitions.forEach(propertyDefinition => {
            formElements.forEach(formElement => {
                if (formElement.type != propertyDefinition.type) {
                    return;
                }

                container.appendChild(this.createFormElement(target, propertyDefinition, formElement));
            });
        });

        return container;
    }

    createFormElement(target, propertyDefinition, formElement) {
        return formElement.create(propertyDefinition, () => target[propertyDefinition.name], value => { if (target[propertyDefinition.name] != value) { target[propertyDefinition.name] = value } });
    }
}