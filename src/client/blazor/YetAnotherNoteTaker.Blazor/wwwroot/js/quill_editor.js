window.startQuill = (body) => {
    var quill = new Quill('#editor-container', {
        modules: {
            formula: true,
            syntax: true,
            toolbar: '#toolbar-container'
        },
        placeholder: '...',
        theme: 'snow'
    });

    document.getElementById('btnTodo').onclick = () => {
        quill.insertText(quill.getLength() - 1, '☐');
        quill.setSelection(quill.getLength() - 1, 0);
    }

    document.querySelector('.ql-editor').innerHTML = body;
}

window.getQuillContent = () => document.querySelector('.ql-editor').innerHTML