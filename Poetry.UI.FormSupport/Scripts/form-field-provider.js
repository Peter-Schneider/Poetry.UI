


/* FORM FIELD PROVIDER */

class FormFieldProvider {
    getFor(formId) {
        return fetch(`Form/Field/GetAllForForm?id=${formId}`, {
            credentials: 'include'
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`${response.status} (${response.statusText})`);
                }

                return response.json();
            });
    }
}

var injections = injections || [];

injections.push(['formFieldProvider', new FormFieldProvider()]);