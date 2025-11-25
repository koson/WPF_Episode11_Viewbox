# สคริปต์การสอน: WPF Episode 11 - Viewbox

## เนื้อหาที่จะสอน

### 1. Viewbox คืออะไร
- Control สำหรับปรับขนาดเนื้อหาอัตโนมัติ
- ทำให้เนื้อหาพอดีกับพื้นที่
- รองรับ Responsive Design

### 2. Viewbox Stretch Modes
- Uniform - รักษาสัดส่วน ขยายให้พอดี
- Fill - ยืดเต็มพื้นที่ (อาจบิดเบี้ยว)
- UniformToFill - รักษาสัดส่วน เต็มพื้นที่ (อาจตัด)
- None - ขนาดเดิม

### 3. การใช้งาน
- Responsive Icons
- Scalable Logos
- Dashboard Widgets
- Buttons ที่ขยายได้

---

## ส่วนที่ 1: Introduction (0:00 - 2:00)

**สวัสดีครับทุกคน**

ยินดีต้อนรับกลับมาสู่ WPF Tutorial Series ของเรา

วันนี้เราจะมาเรียนรู้เกี่ยวกับ **Viewbox** ซึ่งเป็น Control ที่ทรงพลังมาก!

Viewbox ทำอะไร?
- ปรับขนาดเนื้อหาอัตโนมัติ
- ทำให้เนื้อหาพอดีกับพื้นที่
- รองรับ Responsive Design

**เหมือนมีเลนส์ซูมอัตโนมัติ!**

ไม่ว่าพื้นที่จะเล็กหรือใหญ่ Viewbox จะปรับขนาดให้เนื้อหาพอดีเสมอ!

---

## ส่วนที่ 2: Viewbox พื้นฐาน (2:00 - 6:00)

### Demo 2.1: ปัญหาที่ Viewbox แก้

**ไม่มี Viewbox:**
```xml
<Border Width="200" Height="100" BorderBrush="Black" BorderThickness="2">
    <TextBlock Text="Large Text" FontSize="48"/>
</Border>
```

ปัญหา: Text อาจเกินกรอบ หรือเล็กเกินไป!

**มี Viewbox:**
```xml
<Border Width="200" Height="100" BorderBrush="Black" BorderThickness="2">
    <Viewbox>
        <TextBlock Text="Large Text" FontSize="48"/>
    </Viewbox>
</Border>
```

Viewbox ปรับขนาด Text ให้พอดีกับกรอบอัตโนมัติ!

### Demo 2.2: ทดสอบ Resize

```xml
<Border Width="100" Height="50" BorderBrush="Blue" BorderThickness="2" Margin="5">
    <Viewbox>
        <TextBlock Text="HELLO" FontSize="50" FontWeight="Bold"/>
    </Viewbox>
</Border>

<Border Width="200" Height="100" BorderBrush="Blue" BorderThickness="2" Margin="5">
    <Viewbox>
        <TextBlock Text="HELLO" FontSize="50" FontWeight="Bold"/>
    </Viewbox>
</Border>

<Border Width="400" Height="200" BorderBrush="Blue" BorderThickness="2" Margin="5">
    <Viewbox>
        <TextBlock Text="HELLO" FontSize="50" FontWeight="Bold"/>
    </Viewbox>
</Border>
```

**เห็นไหมครับ:**
- Text ขนาดเดียวกัน (FontSize="50")
- แต่แสดงผลต่างกัน ปรับตามพื้นที่!
- รักษาสัดส่วนเสมอ

---

## ส่วนที่ 3: Stretch Modes (6:00 - 15:00)

### Demo 3.1: Stretch="Uniform" (Default)

**Uniform** = รักษาสัดส่วน ขยายให้พอดีที่สุด

```xml
<Border Width="300" Height="150" BorderBrush="Purple" BorderThickness="2">
    <Viewbox Stretch="Uniform">
        <TextBlock Text="Uniform" FontSize="30" FontWeight="Bold"/>
    </Viewbox>
</Border>
```

**ลักษณะ:**
- รักษา Aspect Ratio (สัดส่วน)
- ขยายให้ใหญ่ที่สุดเท่าที่จะพอดี
- ไม่บิดเบี้ยว
- อาจมี Space เหลือ

### Demo 3.2: Stretch="Fill"

**Fill** = ยืดเต็มพื้นที่ ไม่สนใจสัดส่วน

```xml
<Border Width="400" Height="100" BorderBrush="Orange" BorderThickness="2">
    <Viewbox Stretch="Fill">
        <Button Content="🎯 Fill Mode Button" 
                Padding="20,10" 
                FontSize="16" 
                FontWeight="Bold"/>
    </Viewbox>
</Border>
```

**ลักษณะ:**
- ยืดเต็มพื้นที่ทั้งหมด
- **อาจบิดเบี้ยว** (Distort)
- ไม่มี Space เหลือ
- ใช้เมื่อต้องการเต็มพื้นที่แน่นอน

### Demo 3.3: Stretch="UniformToFill"

**UniformToFill** = รักษาสัดส่วน ขยายให้เต็ม **อาจตัดออก**

```xml
<Border Width="300" Height="100" 
        BorderBrush="Teal" BorderThickness="2"
        ClipToBounds="True">
    <Viewbox Stretch="UniformToFill">
        <StackPanel Orientation="Horizontal" Background="LightBlue">
            <TextBlock Text="📷" FontSize="40" Margin="10"/>
            <TextBlock Text="This might get cropped!" 
                       FontSize="24" 
                       FontWeight="Bold"
                       Margin="10"/>
        </StackPanel>
    </Viewbox>
</Border>
```

**ลักษณะ:**
- รักษา Aspect Ratio
- ขยายให้เต็มพื้นที่
- **ส่วนเกินจะถูกตัด** (Crop)
- เหมาะกับรูปภาพที่ต้องการเต็มกรอบ

### Demo 3.4: Stretch="None"

**None** = ไม่ Scale ใช้ขนาดเดิม

```xml
<Border Width="400" Height="100" BorderBrush="Gray" BorderThickness="2">
    <Viewbox Stretch="None">
        <TextBlock Text="Original Size!" 
                   FontSize="20" 
                   FontWeight="Bold"/>
    </Viewbox>
</Border>
```

**ลักษณะ:**
- ขนาดเดิม ไม่เปลี่ยนแปลง
- อาจเล็กเกินไป หรือเกินกรอบ
- ใช้น้อย

### Demo 3.5: เปรียบเทียบทั้ง 4 Modes

```xml
<UniformGrid Rows="2" Columns="2">
    <!-- Uniform -->
    <Border BorderBrush="Blue" BorderThickness="2" Margin="5">
        <Viewbox Stretch="Uniform">
            <Rectangle Width="100" Height="50" Fill="Blue"/>
        </Viewbox>
    </Border>
    
    <!-- Fill -->
    <Border BorderBrush="Orange" BorderThickness="2" Margin="5">
        <Viewbox Stretch="Fill">
            <Rectangle Width="100" Height="50" Fill="Orange"/>
        </Viewbox>
    </Border>
    
    <!-- UniformToFill -->
    <Border BorderBrush="Green" BorderThickness="2" Margin="5" ClipToBounds="True">
        <Viewbox Stretch="UniformToFill">
            <Rectangle Width="100" Height="50" Fill="Green"/>
        </Viewbox>
    </Border>
    
    <!-- None -->
    <Border BorderBrush="Gray" BorderThickness="2" Margin="5">
        <Viewbox Stretch="None">
            <Rectangle Width="100" Height="50" Fill="Gray"/>
        </Viewbox>
    </Border>
</UniformGrid>
```

---

## ส่วนที่ 4: Responsive Icons (15:00 - 20:00)

### Demo 4.1: Icon Scaling

```xml
<UniformGrid Columns="4">
    <!-- Small Icon -->
    <Border Width="60" Height="60" 
            BorderBrush="Red" BorderThickness="2" 
            CornerRadius="30" Margin="5">
        <Viewbox>
            <Grid Width="100" Height="100">
                <Ellipse Fill="Red"/>
                <TextBlock Text="❤️" 
                           FontSize="60" 
                           HorizontalAlignment="Center" 
                           VerticalAlignment="Center"/>
            </Grid>
        </Viewbox>
    </Border>
    
    <!-- Medium Icon -->
    <Border Width="80" Height="80" 
            BorderBrush="Blue" BorderThickness="2" 
            CornerRadius="40" Margin="5">
        <Viewbox>
            <Grid Width="100" Height="100">
                <Ellipse Fill="Blue"/>
                <TextBlock Text="⭐" 
                           FontSize="60" 
                           HorizontalAlignment="Center" 
                           VerticalAlignment="Center"/>
            </Grid>
        </Viewbox>
    </Border>
    
    <!-- Large Icon -->
    <Border Width="100" Height="100" 
            BorderBrush="Green" BorderThickness="2" 
            CornerRadius="50" Margin="5">
        <Viewbox>
            <Grid Width="100" Height="100">
                <Ellipse Fill="Green"/>
                <TextBlock Text="✓" 
                           FontSize="60" 
                           Foreground="White"
                           HorizontalAlignment="Center" 
                           VerticalAlignment="Center"/>
            </Grid>
        </Viewbox>
    </Border>
    
    <!-- Extra Large Icon -->
    <Border Width="120" Height="120" 
            BorderBrush="Purple" BorderThickness="2" 
            CornerRadius="60" Margin="5">
        <Viewbox>
            <Grid Width="100" Height="100">
                <Ellipse Fill="Purple"/>
                <TextBlock Text="🚀" 
                           FontSize="60" 
                           HorizontalAlignment="Center" 
                           VerticalAlignment="Center"/>
            </Grid>
        </Viewbox>
    </Border>
</UniformGrid>
```

**ข้อดี:**
- สร้าง Icon ครั้งเดียว
- ใช้ได้หลายขนาด
- คมชัดเสมอ

---

## ส่วนที่ 5: Scalable Logo (20:00 - 24:00)

### Demo 5.1: Company Logo

```xml
<StackPanel>
    <!-- Logo in Header (Small) -->
    <Border Background="Navy" Padding="10" Height="60">
        <Viewbox Stretch="Uniform" HorizontalAlignment="Left">
            <StackPanel Orientation="Horizontal">
                <Ellipse Width="40" Height="40" Fill="Orange"/>
                <TextBlock Text="MyCompany" 
                           Foreground="White" 
                           FontSize="24" 
                           FontWeight="Bold" 
                           Margin="10,0,0,0"
                           VerticalAlignment="Center"/>
            </StackPanel>
        </Viewbox>
    </Border>
    
    <!-- Logo in Content (Large) -->
    <Border Background="White" Padding="20" Height="200">
        <Viewbox Stretch="Uniform">
            <StackPanel Orientation="Horizontal">
                <Ellipse Width="40" Height="40" Fill="Orange"/>
                <TextBlock Text="MyCompany" 
                           Foreground="Navy" 
                           FontSize="24" 
                           FontWeight="Bold" 
                           Margin="10,0,0,0"
                           VerticalAlignment="Center"/>
            </StackPanel>
        </Viewbox>
    </Border>
</StackPanel>
```

**Logo เดียว ใช้ได้หลายที่!**

### Demo 5.2: Responsive Button

```xml
<UniformGrid Columns="3">
    <Border Height="50" Margin="5">
        <Viewbox Stretch="Uniform">
            <Button Content="Click Me" 
                    Padding="20,10" 
                    FontSize="16" 
                    FontWeight="Bold"
                    Background="DodgerBlue" 
                    Foreground="White"/>
        </Viewbox>
    </Border>
    
    <Border Height="80" Margin="5">
        <Viewbox Stretch="Uniform">
            <Button Content="Click Me" 
                    Padding="20,10" 
                    FontSize="16" 
                    FontWeight="Bold"
                    Background="DodgerBlue" 
                    Foreground="White"/>
        </Viewbox>
    </Border>
    
    <Border Height="120" Margin="5">
        <Viewbox Stretch="Uniform">
            <Button Content="Click Me" 
                    Padding="20,10" 
                    FontSize="16" 
                    FontWeight="Bold"
                    Background="DodgerBlue" 
                    Foreground="White"/>
        </Viewbox>
    </Border>
</UniformGrid>
```

---

## ส่วนที่ 6: Dashboard Widgets (24:00 - 28:00)

### Demo 6.1: Stats Widget

```xml
<Border BorderBrush="LightGray" 
        BorderThickness="1" 
        CornerRadius="10"
        Background="White"
        Padding="10"
        Width="200" 
        Height="150">
    <Viewbox Stretch="Uniform">
        <StackPanel Width="200" Height="150">
            <TextBlock Text="💰 Revenue" 
                       FontSize="18" 
                       FontWeight="Bold" 
                       Foreground="#666"/>
            <TextBlock Text="$45,678" 
                       FontSize="36" 
                       FontWeight="Bold" 
                       Foreground="Green" 
                       Margin="0,10"/>
            <TextBlock Text="+12% this month" 
                       FontSize="14" 
                       Foreground="#999"/>
        </StackPanel>
    </Viewbox>
</Border>
```

Widget จะปรับขนาดตาม Container!

### Demo 6.2: Clock Widget

```xml
<Border BorderBrush="Navy" 
        BorderThickness="2" 
        CornerRadius="100"
        Background="White"
        Width="150" 
        Height="150">
    <Viewbox Stretch="Uniform">
        <Grid Width="100" Height="100">
            <Ellipse Stroke="Navy" StrokeThickness="3"/>
            <TextBlock Text="12:34" 
                       FontSize="24" 
                       FontWeight="Bold" 
                       Foreground="Navy"
                       HorizontalAlignment="Center" 
                       VerticalAlignment="Center"/>
        </Grid>
    </Viewbox>
</Border>
```

---

## ส่วนที่ 7: Use Cases (28:00 - 33:00)

### 7.1 Responsive Game UI

```xml
<Viewbox Stretch="Uniform">
    <Grid Width="800" Height="600" Background="Black">
        <!-- Game UI ที่ปรับขนาดตาม Window -->
        <TextBlock Text="SCORE: 9999" 
                   Foreground="White" 
                   FontSize="32" 
                   FontWeight="Bold"
                   HorizontalAlignment="Right" 
                   VerticalAlignment="Top" 
                   Margin="20"/>
        
        <TextBlock Text="LEVEL 5" 
                   Foreground="Yellow" 
                   FontSize="48" 
                   FontWeight="Bold"
                   HorizontalAlignment="Center" 
                   VerticalAlignment="Top" 
                   Margin="20"/>
    </Grid>
</Viewbox>
```

### 7.2 Print Preview

```xml
<Border BorderBrush="Gray" BorderThickness="1" Height="400">
    <Viewbox Stretch="Uniform">
        <Border Width="210" Height="297" 
                Background="White" 
                BorderBrush="Black" 
                BorderThickness="1">
            <StackPanel Margin="20">
                <TextBlock Text="Document Title" 
                           FontSize="24" 
                           FontWeight="Bold"/>
                <TextBlock Text="This is a print preview..." 
                           FontSize="12" 
                           Margin="0,10" 
                           TextWrapping="Wrap"/>
            </StackPanel>
        </Border>
    </Viewbox>
</Border>
```

เห็นเอกสาร A4 แบบ Preview!

### 7.3 Thumbnail Generator

```xml
<UniformGrid Columns="4">
    <Border Width="100" Height="100" 
            BorderBrush="Gray" BorderThickness="1" 
            Margin="5">
        <Viewbox Stretch="Uniform">
            <Border Width="800" Height="600" Background="LightBlue">
                <TextBlock Text="Page 1" 
                           FontSize="48" 
                           HorizontalAlignment="Center" 
                           VerticalAlignment="Center"/>
            </Border>
        </Viewbox>
    </Border>
    
    <Border Width="100" Height="100" 
            BorderBrush="Gray" BorderThickness="1" 
            Margin="5">
        <Viewbox Stretch="Uniform">
            <Border Width="800" Height="600" Background="LightGreen">
                <TextBlock Text="Page 2" 
                           FontSize="48" 
                           HorizontalAlignment="Center" 
                           VerticalAlignment="Center"/>
            </Border>
        </Viewbox>
    </Border>
    
    <!-- More thumbnails... -->
</UniformGrid>
```

### 7.4 Zoom Control

```xml
<StackPanel>
    <Slider x:Name="ZoomSlider" 
            Minimum="50" Maximum="200" Value="100" 
            Margin="10"/>
    
    <Border BorderBrush="Gray" BorderThickness="2" Height="300">
        <Viewbox Stretch="Uniform">
            <Border Width="{Binding Value, ElementName=ZoomSlider}" 
                    Height="{Binding Value, ElementName=ZoomSlider}">
                <TextBlock Text="Zoomable Content" 
                           FontSize="24" 
                           FontWeight="Bold"/>
            </Border>
        </Viewbox>
    </Border>
</StackPanel>
```

---

## ส่วนที่ 8: Tips & Best Practices (33:00 - 36:00)

### 8.1 เลือก Stretch Mode ให้เหมาะสม

```xml
<!-- ✅ ดี: Icons, Logos - ใช้ Uniform -->
<Viewbox Stretch="Uniform">
    <Image Source="logo.png"/>
</Viewbox>

<!-- ✅ ดี: Background Images - ใช้ UniformToFill -->
<Viewbox Stretch="UniformToFill">
    <Image Source="background.jpg"/>
</Viewbox>

<!-- ⚠️ ระวัง: Fill อาจบิดเบี้ยว -->
<Viewbox Stretch="Fill">
    <Image Source="photo.jpg"/>  <!-- อาจบิด -->
</Viewbox>
```

### 8.2 กำหนดขนาดที่ชัดเจน

```xml
<!-- ✅ ดี: กำหนดขนาด Viewbox -->
<Viewbox Width="200" Height="150" Stretch="Uniform">
    <TextBlock Text="Content"/>
</Viewbox>

<!-- ⚠️ ระวัง: ไม่กำหนดขนาด อาจใหญ่เกินไป -->
<Viewbox Stretch="Uniform">
    <TextBlock Text="Content" FontSize="100"/>
</Viewbox>
```

### 8.3 Performance

```xml
<!-- ✅ ดี: Static Content -->
<Viewbox>
    <Image Source="logo.png"/>
</Viewbox>

<!-- ⚠️ ระวัง: Complex Animations อาจช้า -->
<Viewbox>
    <MediaElement Source="video.mp4"/>  <!-- อาจช้า -->
</Viewbox>
```

### 8.4 Viewbox ใน Viewbox

```xml
<!-- ❌ หลีกเลี่ยง: Nested Viewbox -->
<Viewbox>
    <Viewbox>
        <TextBlock Text="Nested"/>
    </Viewbox>
</Viewbox>
```

ไม่จำเป็น และอาจสับสน!

---

## ส่วนที่ 9: Wrap Up และ Outro (36:00 - 38:00)

**สรุปสิ่งที่เราได้เรียนรู้วันนี้:**

1. ✅ Viewbox = ปรับขนาดเนื้อหาอัตโนมัติ
2. ✅ Stretch Modes:
   - Uniform (รักษาสัดส่วน, พอดี)
   - Fill (เต็มพื้นที่, อาจบิด)
   - UniformToFill (รักษาสัดส่วน, อาจตัด)
   - None (ขนาดเดิม)
3. ✅ Use Cases: Icons, Logos, Widgets, Print Preview
4. ✅ Responsive Design ง่ายขึ้น

**Viewbox เหมาะสำหรับ:**
- Responsive Icons
- Scalable Logos
- Dashboard Widgets
- Game UI
- Print Preview
- Thumbnails

**จุดเด่นของ Viewbox:**
- ปรับขนาดอัตโนมัติ
- รักษาสัดส่วน
- Responsive
- ใช้ง่าย

**ในตอนต่อไป:**

เราจะมาเรียนรู้เกี่ยวกับ **Expander** ซึ่งเป็น Control สำหรับ
สร้าง Collapsible Panel ที่ขยาย-ย่อได้ เหมาะสำหรับประหยัดพื้นที่!

**อย่าลืม:**
- กด Like ถ้าชอบ
- Subscribe เพื่อติดตามตอนต่อไป
- Comment บอกว่าอยากเรียนเรื่องอะไรต่อไป

**ขอบคุณที่รับชมครับ แล้วพบกันใหม่ตอนหน้า สวัสดีครับ!**

---

## เอกสารอ้างอิง

### Official Documentation
- [Viewbox Class - Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.viewbox)
- [Stretch Enumeration - Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.stretch)

### Properties Reference
```
Stretch: Stretch Enum
  - None: ไม่ Scale
  - Fill: เต็มพื้นที่ (อาจบิด)
  - Uniform: รักษาสัดส่วน พอดีภายใน (Default)
  - UniformToFill: รักษาสัดส่วน เต็มพื้นที่ (อาจตัด)

StretchDirection: StretchDirection Enum
  - UpOnly: ขยายเท่านั้น
  - DownOnly: ย่อเท่านั้น
  - Both: ทั้งขยายและย่อ (Default)

Child: UIElement (Element ภายใน)
```

---

## Stretch Mode Comparison

| Mode | รักษาสัดส่วน | เต็มพื้นที่ | อาจบิด | อาจตัด | ใช้เมื่อ |
|------|-------------|----------|--------|--------|---------|
| **Uniform** | ✅ | ❌ | ❌ | ❌ | Icons, Logos |
| **Fill** | ❌ | ✅ | ✅ | ❌ | Backgrounds (ไม่สนใจสัดส่วน) |
| **UniformToFill** | ✅ | ✅ | ❌ | ✅ | Cover Images |
| **None** | ✅ | ❌ | ❌ | ❌ | Fixed Size |

---

## Tips & Best Practices

1. **Default to Uniform**: ใช้ Uniform เป็นค่าเริ่มต้น
2. **ClipToBounds**: ใช้กับ UniformToFill เพื่อตัดส่วนเกิน
3. **Set Container Size**: กำหนดขนาด Container ให้ชัดเจน
4. **Vector Graphics**: Viewbox เหมาะมากกับ Vector (Path, Shape)

---

## Common Mistakes (ข้อผิดพลาดที่พบบ่อย)

### ❌ ใช้ Fill กับ Images
```xml
<!-- ผิด: รูปภาพจะบิด -->
<Viewbox Stretch="Fill">
    <Image Source="photo.jpg"/>
</Viewbox>
```

### ✅ ถูกต้อง
```xml
<Viewbox Stretch="Uniform">
    <Image Source="photo.jpg"/>
</Viewbox>
```

### ❌ ไม่กำหนดขนาด Container
```xml
<!-- ผิด: Viewbox อาจใหญ่เกินไป -->
<Viewbox>
    <TextBlock Text="Huge Text" FontSize="200"/>
</Viewbox>
```

### ✅ ถูกต้อง
```xml
<Viewbox Width="200" Height="100">
    <TextBlock Text="Huge Text" FontSize="200"/>
</Viewbox>
```

### ❌ Viewbox ซ้อน Viewbox
```xml
<!-- ผิด: ไม่จำเป็น -->
<Viewbox>
    <Viewbox>
        <TextBlock Text="Content"/>
    </Viewbox>
</Viewbox>
```

### ✅ ถูกต้อง
```xml
<Viewbox>
    <TextBlock Text="Content"/>
</Viewbox>
```

---

## Code Examples Repository

Source code สำหรับ Episode นี้สามารถดาวน์โหลดได้ที่:
- GitHub: [WPF_Episode11_Viewbox](https://github.com/koson/WPF_Episode11_Viewbox)

---

**End of Script**