import 'package:json_annotation/json_annotation.dart';
part 'payment_details_insert_request.g.dart';

@JsonSerializable()
class PaymetDetailsInsertRequest {
  DateTime? date;
  String? paymentType;
  String? currency;
  String? stripeId;

  PaymetDetailsInsertRequest();

  factory PaymetDetailsInsertRequest.fromJson(Map<String, dynamic> json) =>
      _$PaymetDetailsInsertRequestFromJson(json);

  /// Connect the generated [_$PaymetDetailsInsertRequestToJson] function to the `toJson` method.
  Map<String, dynamic> toJson() => _$PaymetDetailsInsertRequestToJson(this);
}
