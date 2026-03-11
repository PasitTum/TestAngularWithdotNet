import { Component, Input, OnChanges, ViewChild, ElementRef } from '@angular/core';
import QRCode from 'qrcode';

@Component({
  selector: 'app-qr-code',
  standalone: true,
  template: `<canvas #canvas></canvas>`,
})
export class QrCodeComponent implements OnChanges {
  @Input() value = '';
  @Input() size = 200;

  @ViewChild('canvas', { static: true }) canvas!: ElementRef<HTMLCanvasElement>;

  ngOnChanges(): void {
    if (this.value) {
      QRCode.toCanvas(this.canvas.nativeElement, this.value, {
        width: this.size,
        margin: 2,
      });
    }
  }
}
