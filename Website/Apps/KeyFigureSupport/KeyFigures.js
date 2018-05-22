


/* APP */

portal.addApp(class KeyFigures extends App {
    constructor(container) {
        super();

        container.inject('app', this);

        this.addBlade(ListKeyFigures);
        this.addBlade(EditKeyFigure);
    }
});



/* LIST KEY FIGURES */

class ListKeyFigures extends Blade {
    constructor(app, translations) {
        super();

        new PortalHeading(translations.KeyFigures).appendTo(this.root);

        var blade = this;

        var keyFiguresList = new DataTable(
            'key-figure',
            [
                translations.Key,
                translations.Value
            ],
            [
                (dataTable, element, item) => element.innerText = item.Key,
                (dataTable, element, item) => element.innerText = item.Value
            ],
            (dataTable, element, item) => {
                var edit = document.createElement('data-table-edit-button');
                edit.tabIndex = 0;
                edit.innerText = translations.Edit;
                edit.addEventListener('click', event => {
                    app.closeBladesAfter(blade);
                    var editKeyFigureBlade = app.openBlade('EditKeyFigure', item);
                    editKeyFigureBlade.addEventListener('close', message => {
                        if (message == 'saved') {
                            dataTable.update();
                        }
                    });
                });
                element.appendChild(edit);

                var link = document.createElement('a');
                link.classList.add('data-table-edit-button');
                link.innerText = translations.Open;
                link.setAttribute('href', `/KeyFigure/${item.Id}`);
                link.setAttribute('target', '_blank');
                element.appendChild(link);

            }
        );

        keyFiguresList.appendTo(this.root);

        var newKeyFigure = () => {
            app.closeBladesAfter(this);
            var editKeyFigureBlade = app.openBlade('EditKeyFigure');
            editKeyFigureBlade.addEventListener('close', message => {
                if (message == 'saved') {
                    keyFiguresList.update();
                }
            });
        };

        new PortalButton(translations.New, newKeyFigure).appendTo(this.root);

        var close = () => {
            app.closeBladesAfter(this);
            app.closeBlade(this);
        };

        new PortalButton(translations.Close, close).appendTo(this.root);
    }
}



/* EDIT KEY FIGURE */

class EditKeyFigure extends Blade {
    constructor(app, translations, formBuilder, formFieldTypes, formFieldProvider) {
        super();

        this.translations = translations;
        this.formBuilder = formBuilder;
        this.formFieldTypes = formFieldTypes;
        this.formFieldProvider = formFieldProvider;
        this.formFields = this.formFieldProvider.getFor('key-figure');
        
        this.heading = new PortalHeading(translations.KeyFigures).appendTo(this.root);

        var formRoot = document.createElement('div');
        this.root.appendChild(formRoot);
        this.formRoot = formRoot;

        var save = keyFigure => {
            return fetch('/Apps/KeyFigures/Save', {
                credentials: 'include',
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(keyFigure)
            });
        }

        new PortalButton(translations.Cancel, () => app.closeBlade(this)).appendTo(this.root);
        new PortalButton(translations.Save, () => save(this.model).then(() => app.closeBlade(this, 'saved'))).appendTo(this.root);
    }

    open(keyFigure) {
        this.model = keyFigure || {};

        this.formFields
            .then(formFields => {
                var form = this.formBuilder.build(this.model, formFields, this.formFieldTypes, this.translations);

                this.formRoot.appendChild(form);

                if (keyFigure) {
                    this.heading.setText(this.translations.EditKeyFigure);
                } else {
                    this.heading.setText(this.translations.NewKeyFigure);
                }
            });
    }
}