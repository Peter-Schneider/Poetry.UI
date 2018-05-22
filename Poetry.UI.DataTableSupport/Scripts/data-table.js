


/* DATA TABLE */

class DataTable {
    constructor(provider, columnNames, createColumns, createActionButton) {
        this.provider = provider;
        this.columnNames = columnNames;
        this.createColumns = createColumns;
        this.createActionButton = createActionButton;

        this.root = document.createElement('data-table');

        this.update();
    }

    update() {
        fetch(`DataTable/Backend/GetAll?provider=${this.provider}`, { credentials: 'include' })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`DataTable backend returned ${response.status} (${response.statusText})`);
                }

                return response.json();
            })
            .then(response => {
                var table = document.createElement('table');
                table.classList.add('data-table');

                var tableHeader = document.createElement('thead');
                table.appendChild(tableHeader);

                var columnHeaderRow = document.createElement('tr');
                tableHeader.appendChild(columnHeaderRow);

                this.columnNames.forEach(name => {
                    var columnHeader = document.createElement('th');
                    columnHeader.classList.add('data-table-column-header');
                    columnHeaderRow.appendChild(columnHeader);

                    columnHeader.innerText = name;
                });

                var editColumnHeader = document.createElement('th');
                editColumnHeader.classList.add('data-table-edit-column-header');
                columnHeaderRow.appendChild(editColumnHeader);

                var tableBody = document.createElement('tbody');
                table.appendChild(tableBody);

                response.Items.forEach(item => {
                    var row = document.createElement('tr');
                    tableBody.appendChild(row);

                    this.createColumns.forEach(callback => {
                        var cell = document.createElement('td');
                        row.appendChild(cell);

                        callback(this, cell, item);
                    });

                    var editCell = document.createElement('td');
                    editCell.classList.add('data-table-edit-column');
                    row.appendChild(editCell);

                    if (this.createActionButton) {
                        this.createActionButton(this, editCell, item);
                    }
                });

                return [table];
            })
            .then(elements => {
                this.root.childNodes.forEach(n => this.root.removeChild(n));

                elements.forEach(n => this.root.appendChild(n));
            });
    }
}