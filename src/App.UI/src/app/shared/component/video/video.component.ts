import { Component, Input, OnInit, ViewChild, ElementRef } from '@angular/core';


@Component({
  selector: 'app-video',
  templateUrl: './video.component.html',
  styleUrls: ['./video.component.scss']
})
export class VideoComponent implements OnInit {

  @Input() width = 480;
  @Input() showControls = true;
  @Input() showAPIVideo = false;
  @Input() showCarVideo = false;
  @Input() showPoster = false;

  @ViewChild('video') private videoHtml: ElementRef<HTMLVideoElement>;

  private scale = 1;

  constructor() { }

  ngOnInit(): void {
  }

  takePicture() {
    return this.captureImage();
    // implement take picture
    // return harcodedBase64Image;
  }

  private captureImage = () => {
    const video = this.videoHtml.nativeElement;
    const canvas = document.createElement('canvas');
    canvas.width = video.videoWidth * this.scale;
    canvas.height = video.videoHeight * this.scale;
    canvas.getContext('2d')
          .drawImage(video, 0, 0, canvas.width, canvas.height);

    // const img = document.createElement('img');
    // img.src = canvas.toDataURL();
    const base64Data = canvas.toDataURL();
    return base64Data;
}

}
