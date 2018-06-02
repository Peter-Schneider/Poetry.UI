


/* APP */

portal.addApp(new class extends App {
    constructor() {
        super('Product');

        this.translations = translations.scopeTo('Product');
    }

    open() {
        this.openBlade(new ListProducts(this));
    }
});



/* LIST */

class ListProducts extends Blade {
    constructor(app) {
        super();

        this.setTitle(app.translations.get('AllProducts'), new BladeCloseButton(app, this));

        var dataTable;

        this.setToolbar(
            new PortalButton(app.translations.get('New'), () => app.openBlade(new EditProduct(app).onClose(message => dataTable.update()), this)),
        );

        this.setContent(
            dataTable = new DataTable(
                'product',
                [
                    app.translations.get('ArticleNo'),
                    app.translations.get('Name'),
                ],
                [
                    (dataTable, element, item) => element.innerText = item.Item.ArticleNo,
                    (dataTable, element, item) => element.innerText = item.Item.Name,
                ],
                (dataTable, element, item) => {
                    new PortalButton(app.translations.get('Edit'), () => app.openBlade(new EditProduct(app, item.Item).onClose(message => dataTable.update()), this)).appendTo(element);
                    new PortalButton(app.translations.get('Open'), () => app.openBlade(new EditProductOnPage(app, item.Item, item.Url), this)).appendTo(element);
                },
            )
        );
    }
}



/* EDIT PRODUCT ON PAGE */

class EditProductOnPage extends Blade {
    constructor(app, item, url) {
        super();

        this.setFullscreen();

        this.setTitle(app.translations.get('Edit') + ' ' + item.Name, new BladeCloseButton(app, this))
        this.setCustomContent(new PageEditor(item, url));
    }
}



/* EDIT */

class EditProduct extends Blade {
    constructor(app, item) {
        super();

        if (!item) {
            item = {};
        }

        this.app = app;
        this.formBuilder = new FormBuilder;
        this.formFieldTypes = formFieldTypes;
        this.formFieldProvider = new FormFieldProvider();
        this.formFields = this.formFieldProvider.getFor('product');

        this.setTitle(app.translations.get('New'), new BladeCloseButton(app, this));

        var save = () => {
            return fetch('/ProductSupport/Save', {
                credentials: 'include',
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(item)
            });
        }

        this.setContent();

        this.setToolbar(
            new PortalButton(app.translations.get('Save'), () => save().then(() => app.closeBlade(this, 'saved'))),
            new PortalButton(app.translations.get('Cancel'), () => app.closeBlade(this)),
        );

        this.formFields
            .then(formFields => this.setContent(this.formBuilder.build(item, formFields, this.formFieldTypes, this.app.translations)));
    }
}