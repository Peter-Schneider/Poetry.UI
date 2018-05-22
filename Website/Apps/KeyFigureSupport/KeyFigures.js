


/* APP */

portal.addApp(class KeyFigures extends App {
    constructor() {
        super();

        this.translations = translations.scopeTo('KeyFigures');

        this.addBlade(ListKeyFigures);
        this.addBlade(EditKeyFigure);
    }
});



/* LIST KEY FIGURES */

class ListKeyFigures extends Blade {
    constructor(app) {
        super();

        new PortalHeading(app.translations.get('KeyFigures')).appendTo(this.root);

        var blade = this;

        var keyFiguresList = new DataTable(
            'key-figure',
            [
                app.translations.get('Key'),
                app.translations.get('Value')
            ],
            [
                (dataTable, element, item) => element.innerText = item.Key,
                (dataTable, element, item) => element.innerText = item.Value
            ],
            (dataTable, element, item) => {
                var edit = document.createElement('portal-small-button');
                edit.tabIndex = 0;
                edit.innerText = app.translations.get('Edit');
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
                link.classList.add('portal-small-button');
                link.innerText = app.translations.get('Open');
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

        new PortalButton(app.translations.get('New'), newKeyFigure).appendTo(this.root);

        var close = () => {
            app.closeBladesAfter(this);
            app.closeBlade(this);
        };

        new PortalButton(app.translations.get('Close'), close).appendTo(this.root);
    }
}



/* EDIT KEY FIGURE */

class EditKeyFigure extends Blade {
    constructor(app) {
        super();

        this.app = app;
        this.formBuilder = new FormBuilder;
        this.formFieldTypes = formFieldTypes;
        this.formFieldProvider = new FormFieldProvider();
        this.formFields = this.formFieldProvider.getFor('key-figure');
        
        this.heading = new PortalHeading(app.translations.get('KeyFigures')).appendTo(this.root);

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

        new PortalButton(app.translations.get('Cancel'), () => app.closeBlade(this)).appendTo(this.root);
        new PortalButton(app.translations.get('Save'), () => save(this.model).then(() => app.closeBlade(this, 'saved'))).appendTo(this.root);
    }

    open(keyFigure) {
        this.model = keyFigure || {};

        this.formFields
            .then(formFields => {
                var form = this.formBuilder.build(this.model, formFields, this.formFieldTypes, this.app.translations);

                this.formRoot.appendChild(form);

                if (keyFigure) {
                    this.heading.setText(this.app.translations.get('EditKeyFigure'));
                } else {
                    this.heading.setText(this.app.translations.get('NewKeyFigure'));
                }
            });
    }
}