import {
  Component,
  Input,
  AfterViewInit,
  OnChanges,
  ViewChild,
  ElementRef,
} from '@angular/core';
import JsBarcode from 'jsbarcode';

@Component({
  selector: 'app-barcode',
  standalone: true,
  template: '<svg #barcodeEl></svg>',
})
export class BarcodeComponent implements AfterViewInit, OnChanges {
  @Input() value = '';
  @ViewChild('barcodeEl') barcodeEl!: ElementRef<SVGElement>;

  ngAfterViewInit(): void {
    this.render();
  }

  ngOnChanges(): void {
    if (this.barcodeEl) this.render();
  }

  private render(): void {
    if (!this.value || !this.barcodeEl) return;
    JsBarcode(this.barcodeEl.nativeElement, this.value, {
      format: 'CODE39',
      displayValue: false,
      height: 36,
      width: 1.2,
      margin: 0,
    });
  }
}
