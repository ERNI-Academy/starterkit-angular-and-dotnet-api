import { VideoComponent } from './video.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [VideoComponent],
  imports: [
    CommonModule,
  ],
  exports: [
    VideoComponent,
  ]
})
export class VideoModule { }
