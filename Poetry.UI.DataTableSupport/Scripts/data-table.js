﻿import PortalButton from '../../Portal/Scripts/button.js';



/* DATA TABLE */

class DataTable {
    constructor() {
        this.columns = [];
        this.page = 1;

        this.element = document.createElement('poetry-ui-data-table');

        this.tableOuter = document.createElement('div');
        this.tableOuter.classList.add('poetry-ui-data-table-outer');
        this.element.appendChild(this.tableOuter);

        var table = document.createElement('table');
        table.classList.add('poetry-ui-data-table');
        this.tableOuter.appendChild(table);

        var tableHeader = document.createElement('thead');
        table.appendChild(tableHeader);

        this.columnHeaderRow = document.createElement('tr');
        tableHeader.appendChild(this.columnHeaderRow);

        this.tableBody = document.createElement('tbody');
        table.appendChild(this.tableBody);

        this.paging = document.createElement('poetry-ui-data-table-paging');
        this.element.appendChild(this.paging);
    }

    setBackend(name) {
        this.backend = new DataTableBackend(name);

        this.update();

        return this;
    }

    addColumn(callback) {
        var column = new DataTableColumn();

        callback(column);

        this.columns.push(column);

        return this;
    }

    update() {
        this.element.classList.add('poetry-ui-loading');
        this.element.classList.remove('poetry-ui-not-loading');

        this.backend.load({
            page: this.page,
            sortBy: this.sortBy,
            sortDirection: this.sortDirection,
        })
            .then(response => {
                this.element.classList.remove('poetry-ui-loading');
                this.element.classList.add('poetry-ui-not-loading');

                if (!this.columnHeaderRow.children.length) {
                    this.columns.forEach(column => {
                        var element = document.createElement('th');
                        element.style.width = column.actionColumn ? '1%' : (Math.floor(100 / this.columns.length * 100) / 100) + '%';

                        if (column.headerGenerator) {
                            var result = column.headerGenerator(this, element);

                            if (result instanceof Node) {
                                element.appendChild(result);
                            } else if (result.appendTo) {
                                result.appendTo(element);
                            } else if (result.element instanceof Node) {
                                element.appendChild(result.element);
                            } else {
                                var text = document.createElement(`poetry-ui-data-table-header-text`);
                                text.innerText = result;
                                element.appendChild(text);
                            }
                        }

                        if (column.sorting) {
                            var sorter = document.createElement('poetry-ui-data-table-sorter');

                            sorter.addEventListener('click', () => {
                                [...this.columnHeaderRow.querySelectorAll('poetry-ui-data-table-sorter')].forEach(e => e.classList.remove('poetry-ui-active', 'poetry-ui-descending'));

                                if (this.sortBy == column.sorting && this.sortDirection == 'descending') {
                                    this.sortBy = null;
                                    this.sortDirection = null;

                                    this.update();

                                    return;
                                }

                                sorter.classList.add('poetry-ui-active');

                                if (this.sortBy != column.sorting) {
                                    this.sortBy = column.sorting;
                                    this.sortDirection = 'ascending';

                                    sorter.classList.remove('poetry-ui-descending');

                                    this.update();

                                    return;
                                }

                                this.sortDirection = this.sortDirection == 'ascending' ? 'descending' : 'ascending';

                                if (this.sortDirection == 'descending') {
                                    sorter.classList.add('poetry-ui-descending');
                                }

                                this.update();
                            });

                            element.appendChild(sorter);
                        }

                        this.columnHeaderRow.appendChild(element);
                    });
                }

                [...this.tableBody.children].forEach(c => this.tableBody.removeChild(c));
                
                response.Items.forEach(item => {
                    var row = document.createElement('tr');
                    this.tableBody.appendChild(row);

                    this.columns.forEach(column => {
                        var element = document.createElement('td');

                        if (column.actionColumn) {
                            element.classList.add('poetry-ui-data-table-edit-column');
                        }

                        var result = column.contentGenerator(item, this, element);

                        if (result instanceof Node) {
                            element.appendChild(result);
                        } else if (result.appendTo) {
                            result.appendTo(element);
                        } else if (result.element instanceof Node) {
                            element.appendChild(result.element);
                        } else {
                            var text = document.createElement(`poetry-ui-data-table-text`);
                            text.innerText = result;
                            element.appendChild(text);
                        }

                        row.appendChild(element);
                    });
                });

                [...this.paging.children].forEach(c => this.paging.removeChild(c));

                var pages = Array(response.PageCount).fill().map((a, i) => i + 1);
                var pagination = [];

                var startIndex = 0;

                if (this.page > 3) {
                    pagination.push(pages[0]);
                    pagination.push('...');
                    startIndex = this.page - 2;
                    startIndex = Math.max(startIndex, 2);
                    startIndex = Math.min(startIndex, pages.length - 4);
                }

                var stopIndex = Math.min(startIndex + 3, pages.length - 1);

                for (var i = startIndex; i <= stopIndex; i++) {
                    pagination.push(pages[i]);
                }

                if (stopIndex < pages.length - 3) {
                    pagination.push('...');
                    pagination.push(pages[pages.length - 1]);
                } else {

                    for (var i = stopIndex + 1; i < pages.length; i++) {
                        pagination.push(pages[i]);
                    }
                }

                new PortalButton('', () => {
                    if (this.page == 1) {
                        return;
                    }

                    this.page = this.page - 1;
                    this.update();
                }).addClass('poetry-ui-data-table-paging-previous').setDisabled(this.page == 1).appendTo(this.paging);

                pagination.forEach(page => {
                    new PortalButton(page, () => {
                        if (page == '...') {
                            return;
                        }

                        this.page = page;
                        this.update();
                    }).addClass('poetry-ui-portal-button-active', this.page == page).appendTo(this.paging);
                });

                new PortalButton('', () => {
                    if (this.page == response.PageCount) {
                        return;
                    }

                    this.page = this.page + 1;
                    this.update();
                }).addClass('poetry-ui-data-table-paging-next').setDisabled(this.page == response.PageCount).appendTo(this.paging);
            });
    }

    appendTo(element) {
        element.appendChild(this.element);
    }
}



/* BACKEND */

class DataTableBackend {
    constructor(name) {
        this.name = name;
    }

    load(query) {
        var sort = query.sortBy ? `&sortby=${query.sortBy}&sortdirection=${query.sortDirection}` : '';

        return fetch(`DataTable/Backend/GetAll?provider=${this.name}&page=${query.page}${sort}`, { credentials: 'include' })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`DataTable backend returned ${response.status} (${response.statusText})`);
                }

                return response.json();
            });
    }
}



/* COLUMN */

class DataTableColumn {
    setActionColumn() {
        this.actionColumn = true;

        return this;
    }

    setHeader(headerGenerator) {
        this.headerGenerator = headerGenerator;

        return this;
    }

    setContent(contentGenerator) {
        this.contentGenerator = contentGenerator;

        return this;
    }

    setSorting(name) {
        this.sorting = name;

        return this;
    }
}



/* COPY AS TAB SEPARATED */

class DataTableCopyAsTabSeparated {
    constructor(dataTable) {
        this.dataTable = dataTable;
    }

    copy() {
        return this.getPages()
            .then(pages => {
                var result = this.getHeaders();

                pages.forEach(page => page.Items.forEach(item => result += this.getRow(item)));

                return navigator.clipboard.writeText(result);
            });
    }

    getPages() {
        return this.dataTable.backend
            .load({
                page: 1,
                sortBy: this.dataTable.sortBy,
                sortDirection: this.dataTable.sortDirection,
            })
            .then(page => [page]);
    }

    getHeaders() {
        var result = [];

        this.dataTable.columns
            .forEach(column => {
                if (column.actionColumn) {
                    return;
                }

                if (column.headerGenerator) {
                    var content = column.headerGenerator(this.dataTable);

                    if (content instanceof Node) {
                        result.push(content.innerText);
                    } else if (content.element instanceof Node) {
                        result.push(content.element.innerText);
                    } else {
                        result.push(content);
                    }
                }
            });

        return result.join('\t') + '\n';
    }

    getRow(item) {
        var result = [];

        this.dataTable.columns
            .forEach(column => {
                if (column.actionColumn) {
                    return;
                }

                if (column.contentGenerator) {
                    var content = column.contentGenerator(item, this.dataTable);

                    if (content instanceof Node) {
                        result.push(content.innerText);
                    } else if (content.element instanceof Node) {
                        result.push(content.element.innerText);
                    } else {
                        result.push(content);
                    }
                }
            });

        return result.join('\t') + '\n';
    }
}

export default DataTable;