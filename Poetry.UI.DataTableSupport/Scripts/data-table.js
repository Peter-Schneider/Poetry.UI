


/* DATA TABLE */

class DataTable {
    constructor(app, blade, backend, translations) {
        this.app = app;
        this.blade = blade;
        this.backend = backend;
        this.translations = translations;

        this.root = document.createElement('data-table');

        this.update();
    }

    update() {
        this.backend
            .getAll()
            .then(keyFigures => {
                var table = document.createElement('table');
                table.classList.add('data-table');

                var columns = ['Key', 'Value'];

                var tableHeader = document.createElement('thead');
                table.appendChild(tableHeader);

                var columnHeaderRow = document.createElement('tr');
                tableHeader.appendChild(columnHeaderRow);

                columns.forEach(name => {
                    var columnHeader = document.createElement('th');
                    columnHeader.classList.add('data-table-column-header');
                    columnHeaderRow.appendChild(columnHeader);

                    columnHeader.innerText = this.translations[name];
                });

                var editColumnHeader = document.createElement('th');
                editColumnHeader.classList.add('data-table-edit-column-header');
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
                    editCell.classList.add('data-table-edit-column');
                    keyFiguresTableRow.appendChild(editCell);

                    var edit = document.createElement('data-table-edit-button');
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