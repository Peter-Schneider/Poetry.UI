///<reference path="../../../Poetry.UI.PortalSupport/Scripts/app.js"/>
///<reference path="../../../Poetry.UI.PortalSupport/Scripts/blade.js"/>
///<reference path="../../../Poetry.UI.PortalSupport/Scripts/button.js"/>
///<reference path="../../../Poetry.UI.PageEditingSupport/Scripts/page-editor.js"/>
///<reference path="../../../Poetry.UI.TranslationSupport/Scripts/translation.js"/>



/* APP */

portal.addApp(new class extends App {
    constructor() {
        super('KeyFigures');

        this.translations = translations.scopeTo('KeyFigures');
    }

    open() {
        this.openBlade(new ListKeyFigures(this));
    }
});



/* LIST KEY FIGURES */

class ListKeyFigures extends Blade {
    constructor(app) {
        super();

        this.setTitle(app.translations.get('KeyFigures'), new BladeCloseButton(app, this));

        var dataTable;

        this.setToolbar(
            new PortalButton(app.translations.get('New'), () => app.openBlade(new EditKeyFigure(app), this).onClose(message => dataTable.update())),
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
                    new PortalButton(app.translations.get('Edit'), () => app.openBlade(new EditKeyFigure(app, item).onClose(message => dataTable.update()), this)).appendTo(element);
                    new PortalButton(app.translations.get('Open'), () => app.openBlade(new EditKeyFigurePage(app, item, `/KeyFigure/${item.Id}`), this)).appendTo(element);
                },
            )
        );
    }
}



/* EDIT KEY FIGURE */

class EditKeyFigurePage extends Blade {
    constructor(app, keyFigure, url) {
        super();

        this.setFullscreen();

        this.setTitle(app.translations.get('Edit') + ' ' + keyFigure.Key, new BladeCloseButton(app, this))
        this.setCustomContent(new PageEditor(keyFigure, url));
    }
}



/* NEW KEY FIGURE */

class EditKeyFigure extends Blade {
    constructor(app, keyFigure) {
        super();

        this.formBuilder = new FormBuilder;
        this.formFieldTypes = formFieldTypes;
        this.formFieldProvider = new FormFieldProvider();
        this.formFields = this.formFieldProvider.getFor('key-figure');
        
        this.setTitle(app.translations.get('NewKeyFigure'), new BladeCloseButton(app, this));

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

        this.model = keyFigure || {};

        this.formFields
            .then(formFields => this.setContent(this.formBuilder.build(this.model, formFields, this.formFieldTypes, app.translations)));
    }
}