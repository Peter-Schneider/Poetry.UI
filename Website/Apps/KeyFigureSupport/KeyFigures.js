


/* APP */

portal.addApp(class KeyFigures extends App {
    constructor(container) {
        super();

        container.inject('app', this);
        container.inject('backend', container.resolve(KeyFiguresBackend));

        this.addBlade(ListKeyFigures);
        this.addBlade(EditKeyFigure);
    }
});



/* BACKEND */

class KeyFiguresBackend {
    getAll() {
        return fetch('/Apps/KeyFigures/GetAll', {
            credentials: 'include'
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`${response.status} (${response.statusText})`);
                }

                return response.json();
            });
    }

    save(keyFigure) {
        return fetch('/Apps/KeyFigures/Save', {
            credentials: 'include',
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(keyFigure)
        });
    }
}



/* LIST */

class KeyFiguresList {
    constructor(app, blade, backend, translations) {
        this.app = app;
        this.blade = blade;
        this.backend = backend;
        this.translations = translations;

        this.root = document.createElement('key-figures-list');

        this.update();
    }

    update() {
        this.backend
            .getAll()
            .then(keyFigures => {
                var table = document.createElement('table');
                table.classList.add('list-table');

                var columns = ['Key', 'Value'];

                var tableHeader = document.createElement('thead');
                table.appendChild(tableHeader);

                var columnHeaderRow = document.createElement('tr');
                tableHeader.appendChild(columnHeaderRow);

                columns.forEach(name => {
                    var columnHeader = document.createElement('th');
                    columnHeader.classList.add('list-column-header');
                    columnHeaderRow.appendChild(columnHeader);

                    columnHeader.innerText = this.translations[name];
                });

                var editColumnHeader = document.createElement('th');
                editColumnHeader.classList.add('list-edit-column-header');
                columnHeaderRow.appendChild(editColumnHeader);

                var tableBody = document.createElement('tbody');
                table.appendChild(tableBody);

                keyFigures.forEach(row => {
                    var keyFiguresTableRow = document.createElement('tr');
                    tableBody.appendChild(keyFiguresTableRow);

                    columns.forEach(name => {
                        var cell = document.createElement('td');
                        keyFiguresTableRow.appendChild(cell);

                        cell.innerText = row[name];
                    });

                    var editCell = document.createElement('td');
                    editCell.classList.add('list-edit-column');
                    keyFiguresTableRow.appendChild(editCell);

                    var edit = document.createElement('list-edit-button');
                    edit.tabIndex = 0;
                    edit.innerText = this.translations.Edit;
                    edit.addEventListener('click', event => {
                        this.app.closeBladesAfter(this.blade);
                        var editKeyFigureBlade = this.app.openBlade('EditKeyFigure', row);
                        editKeyFigureBlade.addEventListener('close', message => {
                            if (message == 'saved') {
                                this.update();
                            }
                        });
                    });
                    editCell.appendChild(edit);
                });

                return [table];
            })
            .then(elements => {
                this.root.childNodes.forEach(n => this.root.removeChild(n));

                elements.forEach(n => this.root.appendChild(n));
            });
    }
}



/* LIST KEY FIGURES */

class ListKeyFigures extends Blade {
    constructor(app, container, translations, keyFiguresList) {
        super();

        var heading = document.createElement('h1');
        heading.className = 'heading';
        heading.innerText = translations.KeyFigures;
        this.root.appendChild(heading);

        var keyFiguresList = container.resolve(KeyFiguresList, { blade: this });
        this.root.appendChild(keyFiguresList.root);

        var newKeyFigure = document.createElement('portal-button');
        newKeyFigure.tabIndex = 0;
        newKeyFigure.innerText = translations.New;
        newKeyFigure.addEventListener('click', () => {
            app.closeBladesAfter(this);
            var editKeyFigureBlade = app.openBlade('EditKeyFigure');
            editKeyFigureBlade.addEventListener('close', message => {
                if (message == 'saved') {
                    keyFiguresList.update();
                }
            });
        });
        this.root.appendChild(newKeyFigure);

        var close = document.createElement('portal-button');
        close.tabIndex = 0;
        close.innerText = translations.Close;
        close.addEventListener('click', () => {
            app.closeBladesAfter(this);
            app.closeBlade(this);
        });
        this.root.appendChild(close);
    }
}



/* EDIT KEY FIGURE */

class EditKeyFigure extends Blade {
    constructor(app, translations, backend, formBuilder, formFieldTypes, formFieldProvider) {
        super();

        this.translations = translations;
        this.backend = backend;
        this.formBuilder = formBuilder;
        this.formFieldTypes = formFieldTypes;
        this.formFieldProvider = formFieldProvider;

        var heading = document.createElement('h1');
        heading.classList.add('heading');
        heading.classList.add('edit-key-figure');
        this.root.appendChild(heading);

        this.heading = heading;

        var formRoot = document.createElement('div');
        this.root.appendChild(formRoot);
        this.formRoot = formRoot;

        var cancel = document.createElement('portal-button');
        cancel.tabIndex = 0;
        cancel.innerText = translations.Cancel;
        cancel.addEventListener('click', () => {
            app.closeBlade(this);
        });
        this.root.appendChild(cancel);

        var save = document.createElement('portal-button');
        save.tabIndex = 0;
        save.innerText = translations.Save;
        save.addEventListener('click', () => {
            this.backend
                .save(this.model)
                .then(() => {
                    app.closeBlade(this, 'saved');
                });
        });
        this.root.appendChild(save);
    }

    open(keyFigure) {
        this.model = keyFigure;

        this.formFieldProvider.getFor('key-figure')
            .then(formFields => {
                var form = this.formBuilder.build(this.model, formFields, this.formFieldTypes, this.translations);

                this.formRoot.appendChild(form);

                if (keyFigure) {
                    this.heading.innerText = this.translations.EditKeyFigure;
                } else {
                    this.heading.innerText = this.translations.NewKeyFigure;
                }
            });
    }
}