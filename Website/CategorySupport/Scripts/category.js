


/* APP */

portal.addApp(new class extends App {
    constructor() {
        super('Category');

        this.translations = translations.scopeTo('Category');
    }

    open() {
        if (this.blades.length) {
            return;
        }

        this.openBlade(new ListCategories(this));
    }
});



/* LIST */

class ListCategories extends Blade {
    constructor(app) {
        super();

        this.setTitle(app.translations.get('AllCategories'), new BladeCloseButton(app, this));

        var dataTable;

        this.setToolbar(
            new PortalButton(app.translations.get('New'), () => app.openBlade(new EditCategory(app).onClose(message => dataTable.update()), this)),
        );

        this.setContent(
            dataTable = new DataTable()
                .setBackend('category')
                .addColumn(item => item.Item.Name)
                .setHeader(element => app.translations.get('Name'))
                .setSorting('Name', true)
                .done()
                .addColumn(item => item.Url)
                .setHeader(element => app.translations.get('UrlSegment'))
                .done()
                .addActionColumn((item, dataTable) => new PortalButton(app.translations.get('Edit'), () => app.openBlade(new EditCategory(app, item.Item).onClose(message => dataTable.update()), this)))
                .done()
                .addActionColumn(item => new PortalButton(app.translations.get('Open'), () => app.openBlade(new EditCategoryOnPage(app, item.Item, item.Url), this)))
                .done()
        );
    }
}



/* EDIT CATEGORY ON PAGE */

class EditCategoryOnPage extends Blade {
    constructor(app, item, url) {
        super();

        this.setFullscreen();

        this.setTitle(app.translations.get('Edit') + ' ' + item.Name, new BladeCloseButton(app, this))
        this.setCustomContent(new PageEditor(item, url));
    }
}



/* EDIT */

class EditCategory extends Blade {
    constructor(app, item) {
        super();

        if (!item) {
            item = {};
        }

        this.app = app;
        this.formBuilder = new FormBuilder;
        this.formFieldTypes = formFieldTypes;
        this.formFieldProvider = new FormFieldProvider();
        this.formFields = this.formFieldProvider.getFor('category');

        this.setTitle(app.translations.get('New'), new BladeCloseButton(app, this));

        var save = () => {
            return fetch('/CategorySupport/Save', {
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