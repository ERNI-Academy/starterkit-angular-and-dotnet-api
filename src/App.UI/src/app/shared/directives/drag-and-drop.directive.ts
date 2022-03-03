import { Directive, Output, Input, EventEmitter, HostBinding, HostListener } from '@angular/core';

/**
 * Based on:
 * https://medium.com/@mariemchabeni/angular-7-drag-and-drop-simple-file-uploadin-in-less-than-5-minutes-d57eb010c0dc
 */
@Directive({
  selector: '[appDragDrop]',
  exportAs: 'appDragDrop',
})
export class DragDropDirective {

  @Output() fileDropped = new EventEmitter<FileList>();
  @Output() userIsDragging = new EventEmitter<boolean>();

  // @HostBinding('style.background-color') private background: string;
  // @HostBinding('style.opacity') private opacity: string;
  @HostBinding('class.dragover') private dragOver: boolean;

  /** The "clearTimeout" variables are necessary because some 'dragleave' events are thrown while still "dragover" events
   * are being thrown (probably a browser issue?).
   * With this we avoid 'blinking' classes applyed and removed.
   */
  private settimeoutWindowDragLeave: any;
  private settimeoutTargetDragLeave: any;

  @HostListener('window:dragover', ['$event'])
  onDragOverWindow(evt: DragEvent) {
    /**
     * preventDefault is necessary so when "dropping" a file the browser does not open a new tab and the event goes to the "host element".
     * Don't know why it must be put on the "dragover" event.
     */
    evt.preventDefault();
    evt.stopPropagation();

    // console.log(`You are dragging`, evt);
    clearTimeout(this.settimeoutWindowDragLeave);
    this.userIsDragging.next(true);
  }

  @HostListener('window:dragleave', ['$event'])
  onDragLeaveWindow(evt: DragEvent) {
    // console.log(`You stopped dragging`, evt);
    // evt.preventDefault();
    // evt.stopPropagation();

    clearTimeout(this.settimeoutWindowDragLeave);
    this.settimeoutWindowDragLeave = setTimeout(() => {
      this.userIsDragging.next(false);
    }, 500);
  }

  @HostListener('window:drop', ['$event'])
  onDropWindow(evt: DragEvent) {
    // console.log(`You stopped dragging`, evt);
    // console.log(`You dropped Window`, evt);
    evt.preventDefault();
    evt.stopPropagation();

    this.userIsDragging.next(false);
    this.dragOver = false;
  }

  // Dragover listener
  @HostListener('dragover', ['$event']) onDragOver(evt: DragEvent) {
    // console.log(`You dragover Host`, evt);
    // evt.preventDefault();
    // evt.stopPropagation();
    clearTimeout(this.settimeoutWindowDragLeave);
    clearTimeout(this.settimeoutTargetDragLeave);
    this.dragOver = true;
  }

  // Dragleave listener
  @HostListener('dragleave', ['$event']) public onDragLeave(evt: any) {
    // console.log(`You dragleave Host`, evt);
    // evt.preventDefault();
    // evt.stopPropagation();
    clearTimeout(this.settimeoutTargetDragLeave);
    this.settimeoutTargetDragLeave = setTimeout(() => {
      this.dragOver = false;
    }, 100);
  }

  // Drop listener
  @HostListener('drop', ['$event']) public ondrop(evt: DragEvent) {
    // console.log(`You dropped Host`, evt);
    evt.preventDefault();
    evt.stopPropagation();
    const files = evt.dataTransfer.files;
    if (files.length > 0) {
      this.fileDropped.emit(files);
    }

    this.userIsDragging.next(false);
    this.dragOver = false;
  }
}
