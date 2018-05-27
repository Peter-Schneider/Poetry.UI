///<reference path="../../../Poetry.UI.PortalSupport/Scripts/app.js"/>
///<reference path="../../../Poetry.UI.PortalSupport/Scripts/blade.js"/>
///<reference path="../../../Poetry.UI.PortalSupport/Scripts/button.js"/>
///<reference path="../../../Poetry.UI.PageEditingSupport/Scripts/page-editor.js"/>
///<reference path="../../../Poetry.UI.TranslationSupport/Scripts/translation.js"/>



/* APP */

portal.addApp(class KeyFigures extends App {
    constructor() {
        super();

        this.translations = translations.scopeTo('KeyFigures');

        this.addBlade(ListKeyFigures);
        this.addBlade(EditKeyFigurePage);
        this.addBlade(EditKeyFigure);
    }
});



/* LIST KEY FIGURES */

class ListKeyFigures extends Blade {
    constructor(app) {
        super();

        this.setTitle(app.translations.get('KeyFigures'));

        var dataTable;

        this.setToolbar(
            new PortalButton(app.translations.get('New'), () => app.closeBladesAfter(this).openBlade('EditKeyFigure').onClose(message => dataTable.update())),
        );

        this.setContent(
            dataTable = new DataTable(
                'key-figure',
                [
                    app.translations.get('Key'),
                    app.translations.get('Value'),
                ],
                [
                    (dataTable, element, item) => element.innerText = item.Key,
                    (dataTable, element, item) => element.innerText = item.Value,
                ],
                (dataTable, element, item) => {
                    new PortalButton(app.translations.get('Edit'), () => app.closeBladesAfter(this).openBlade('EditKeyFigure', item).onClose(message => dataTable.update())).appendTo(element);
                    new PortalButton(app.translations.get('Open'), () => app.closeBladesAfter(this).openBlade('EditKeyFigurePage', item, `/KeyFigure/${item.Id}`)).appendTo(element);
                },
            )
        );
    }
}



/* EDIT KEY FIGURE */

class EditKeyFigurePage extends Blade {
    constructor(app) {
        super(true);

        this.app = app;
    }

    open(keyFigure, url) {
        this.setTitle(this.app.translations.get('Edit') + ' ' + keyFigure.Key)
        this.setCustomContent(new PageEditor(keyFigure, url));
    }
}



/* NEW KEY FIGURE */

class EditKeyFigure extends Blade {
    constructor(app) {
        super();

        this.app = app;
        this.formBuilder = new FormBuilder;
        this.formFieldTypes = formFieldTypes;
        this.formFieldProvider = new FormFieldProvider();
        this.formFields = this.formFieldProvider.getFor('key-figure');
        
        this.setTitle(app.translations.get('NewKeyFigure'));

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

        this.setContent();

        this.setToolbar(
            new PortalButton(app.translations.get('Save'), () => save(this.model).then(() => app.closeBlade(this, 'saved'))),
            new PortalButton(app.translations.get('Cancel'), () => app.closeBlade(this)),
        );
    }

    open(keyFigure) {
        this.model = keyFigure || {};

        this.formFields
            .then(formFields => this.setContent(this.formBuilder.build(this.model, formFields, this.formFieldTypes, this.app.translations)));
    }
}