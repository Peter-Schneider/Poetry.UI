


/* PROPERTY DEFINITION PROVIDER */

class PropertyDefinitionProvider {
    getFor(id) {
        return fetch(`Form/PropertyDefinition/GetAllFor?id=${id}`, {
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