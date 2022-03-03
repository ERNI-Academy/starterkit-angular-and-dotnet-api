import { Component, HostBinding, Input, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';

/**
 * Based on this:
 * https://github.com/MariemChaabeni/angular7-upload-file
 * https://medium.com/@mariemchabeni/angular-7-drag-and-drop-simple-file-uploadin-in-less-than-5-minutes-d57eb010c0dc
 *
 *
 */
@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.scss'],
})
export class UploadFileComponent implements OnInit {
  @Input() allowClickFileSearch = false;
  @Input() closeOnClick = false;
  @Input() onlyOneFile = false;
  @Output() fileDropped = new EventEmitter<File[]>();

  visible = false;

  @HostBinding('class.none') get invisible() { return !this.visible; }
  @ViewChild('nativeInputFile') inputFile: HTMLInputElement;

  files: File[] = [];

  constructor() {}

  ngOnInit(): void {}

  // tslint:disable-next-line: member-ordering
  private timeoutId: any;
  open() {
    if (this.timeoutId) { clearTimeout(this.timeoutId); }

    this.visible = true;
    this.timeoutId = setTimeout(() => {
      this.visible = false;
    }, 3000);
  }

  onClick() {
    if (this.allowClickFileSearch) {
      this.inputFile.click();
    }

    if (this.closeOnClick) {
      this.visible = false;
    }
  }

  uploadFile(fileList: FileList) {
    // tslint:disable-next-line: prefer-for-of
    for (let index = 0; index < fileList.length; index++) {
      const element = fileList[index];
      this.files.push(element);
    }

    if (this.onlyOneFile) {
      // keep just the last file uploaded
      this.files = [this.files[this.files.length - 1]];
    }

    this.fileDropped.next(this.files);
  }
  deleteAttachment(index: number) {
    this.files.splice(index, 1);
  }
}
